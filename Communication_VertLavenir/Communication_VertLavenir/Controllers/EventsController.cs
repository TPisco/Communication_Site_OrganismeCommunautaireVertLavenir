using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Communication_VertLavenir.Services;
using Communication_VertLavenir.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Controllers
{
    public class EventsController : BaseController
    {
        private readonly ICurrentUserService _currentUser;

        public EventsController(AppDbContext db, ICurrentUserService currentUser) : base(db)
        {
            _currentUser = currentUser;
        }

        // Events list (upcoming, public)
        public async Task<IActionResult> Index(string? search)
        {
            var events = await Db.Events
                .Where(e => e.IsActive && e.IsPublic && e.StartDateTime >= DateTime.Now)
                .OrderBy(e => e.StartDateTime)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                events = events.Where(e =>
                    e.TitleLoc.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    e.Location.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var counts = await Db.EventRegistrations
                .Where(r => r.Status == RegistrationStatus.Confirmed)
                .GroupBy(r => r.EventId)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key, x => x.Count);

            var vm = new EventListViewModel
            {
                Search = search,
                Events = events.Select(e => new EventCardViewModel
                {
                    Event = e,
                    ConfirmedCount = counts.TryGetValue(e.Id, out var c) ? c : 0
                }).ToList()
            };
            return View(vm);
        }

        // Monthly calendar
        public async Task<IActionResult> Calendar(int? month, int? year)
        {
            var now = DateTime.Today;
            int m = month ?? now.Month;
            int y = year ?? now.Year;
            if (m < 1) { m = 12; y--; }
            if (m > 12) { m = 1; y++; }

            var start = new DateTime(y, m, 1);
            var end = start.AddMonths(1);

            var events = await Db.Events
                .Where(e => e.IsActive && e.StartDateTime >= start && e.StartDateTime < end)
                .OrderBy(e => e.StartDateTime)
                .ToListAsync();

            return View(new CalendarViewModel { Month = m, Year = y, Events = events });
        }

        // JSON API for an interactive calendar
        [HttpGet("/api/events/calendar")]
        public async Task<IActionResult> CalendarApi(int month, int year)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);

            var events = await Db.Events
                .Where(e => e.IsActive && e.StartDateTime >= start && e.StartDateTime < end)
                .OrderBy(e => e.StartDateTime)
                .ToListAsync();

            var result = events.Select(e => new
            {
                e.Id,
                title = e.TitleLoc,
                date = e.StartDateTime,
                e.Location,
                category = e.Category.ToString()
            });

            return Json(result);
        }

        // Registration form
        [HttpGet]
        public async Task<IActionResult> Register(int id)
        {
            var ev = await Db.Events.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
            if (ev == null) return NotFound();

            var vm = new EventRegisterViewModel { EventId = ev.Id, EventTitle = ev.TitleLoc };

            if (_currentUser.IsAuthenticated && _currentUser.UserId is int uid)
            {
                var user = await Db.Users.FindAsync(uid);
                if (user != null)
                {
                    vm.Name = user.FullName;
                    vm.Email = user.Email;
                    vm.Phone = user.Phone;
                }
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(EventRegisterViewModel vm)
        {
            var ev = await Db.Events.FirstOrDefaultAsync(e => e.Id == vm.EventId && e.IsActive);
            if (ev == null) return NotFound();

            vm.EventTitle = ev.TitleLoc;

            if (!ModelState.IsValid) return View(vm);

            int confirmed = await Db.EventRegistrations
                .CountAsync(r => r.EventId == ev.Id && r.Status == RegistrationStatus.Confirmed);

            var status = confirmed >= ev.Capacity
                ? RegistrationStatus.Waitlisted
                : RegistrationStatus.Confirmed;

            var registration = new EventRegistration
            {
                EventId = ev.Id,
                UserId = _currentUser.UserId,
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Status = status,
                RegistrationDate = DateTime.UtcNow
            };

            Db.EventRegistrations.Add(registration);
            await Db.SaveChangesAsync();

            TempData["Success"] = status == RegistrationStatus.Confirmed
                ? "Votre inscription est confirmée ! / Your registration is confirmed!"
                : "L'évènement est complet : vous êtes sur la liste d'attente. / Event full: you are waitlisted.";

            return RedirectToAction(nameof(Index));
        }
    }
}
