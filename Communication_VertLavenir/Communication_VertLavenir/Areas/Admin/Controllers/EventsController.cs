using System.Text;
using Communication_VertLavenir.Areas.Admin.Models;
using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Areas.Admin.Controllers
{
    public class EventsController : AdminBaseController
    {
        public EventsController(AppDbContext db) : base(db) { }

        public async Task<IActionResult> Index(string? search)
        {
            var query = Db.Events.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(e => e.TitleFr.Contains(search) || e.Location.Contains(search));

            ViewBag.Search = search;
            var events = await query.OrderByDescending(e => e.StartDateTime).ToListAsync();

            ViewBag.Counts = await Db.EventRegistrations
                .Where(r => r.Status == RegistrationStatus.Confirmed)
                .GroupBy(r => r.EventId)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key, x => x.Count);

            return View(events);
        }

        public IActionResult Create() => View(new Event { StartDateTime = DateTime.Today.AddDays(7).AddHours(9), Capacity = 20 });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event model)
        {
            if (!ModelState.IsValid) return View(model);
            model.CreatedAt = DateTime.UtcNow;
            Db.Events.Add(model);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Évènement créé.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ev = await Db.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var ev = await Db.Events.FindAsync(id);
            if (ev == null) return NotFound();

            ev.TitleFr = model.TitleFr; ev.TitleEn = model.TitleEn;
            ev.DescriptionFr = model.DescriptionFr; ev.DescriptionEn = model.DescriptionEn;
            ev.StartDateTime = model.StartDateTime; ev.EndDateTime = model.EndDateTime;
            ev.Location = model.Location; ev.Capacity = model.Capacity;
            ev.Category = model.Category; ev.IsPublic = model.IsPublic; ev.IsActive = model.IsActive;
            ev.UpdatedAt = DateTime.UtcNow;

            await Db.SaveChangesAsync();
            TempData["Success"] = "Évènement mis à jour.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ev = await Db.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ev = await Db.Events.FindAsync(id);
            if (ev != null)
            {
                Db.Events.Remove(ev);
                await Db.SaveChangesAsync();
                TempData["Success"] = "Évènement supprimé.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Registrations(int id)
        {
            var ev = await Db.Events.FindAsync(id);
            if (ev == null) return NotFound();

            var vm = new EventRegistrationsViewModel
            {
                Event = ev,
                Registrations = await Db.EventRegistrations
                    .Where(r => r.EventId == id)
                    .OrderBy(r => r.RegistrationDate)
                    .ToListAsync()
            };
            return View(vm);
        }

        public async Task<IActionResult> ExportCsv(int id)
        {
            var ev = await Db.Events.FindAsync(id);
            if (ev == null) return NotFound();

            var regs = await Db.EventRegistrations
                .Where(r => r.EventId == id)
                .OrderBy(r => r.RegistrationDate)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Name,Email,Phone,Status,RegistrationDate");
            foreach (var r in regs)
                sb.AppendLine($"\"{r.Name}\",\"{r.Email}\",\"{r.Phone}\",{r.Status},{r.RegistrationDate:yyyy-MM-dd HH:mm}");

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"inscriptions-event-{id}.csv");
        }
    }
}
