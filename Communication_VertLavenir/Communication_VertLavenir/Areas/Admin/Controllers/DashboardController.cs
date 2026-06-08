using System.Globalization;
using Communication_VertLavenir.Areas.Admin.Models;
using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        public DashboardController(AppDbContext db) : base(db) { }

        public async Task<IActionResult> Index()
        {
            var donations = await Db.Donations.Where(d => d.Status == DonationStatus.Completed).ToListAsync();
            var members = await Db.Users.Where(u => u.Role == UserRole.Member).ToListAsync();
            var events = await Db.Events.ToListAsync();

            var vm = new AdminDashboardViewModel
            {
                TotalMembers = members.Count,
                TotalEvents = events.Count,
                TotalRegistrations = await Db.EventRegistrations.CountAsync(r => r.Status == RegistrationStatus.Confirmed),
                TotalDonations = donations.Count,
                TotalDonated = donations.Sum(d => d.Amount),
                NewContactMessages = await Db.ContactMessages.CountAsync(c => c.Status == ContactStatus.New),
                CapacityTotal = events.Sum(e => e.Capacity),
                ConfirmedTotal = await Db.EventRegistrations.CountAsync(r => r.Status == RegistrationStatus.Confirmed)
            };

            var months = Enumerable.Range(0, 12)
                .Select(i => DateTime.UtcNow.AddMonths(-11 + i))
                .Select(d => new DateTime(d.Year, d.Month, 1))
                .ToList();

            foreach (var m in months)
            {
                var label = m.ToString("MMM yy", CultureInfo.CurrentUICulture);
                var next = m.AddMonths(1);

                var monthDonations = donations.Where(d => d.CreatedAt >= m && d.CreatedAt < next).ToList();
                vm.DonationsPerMonth.Add(new MonthlyPoint { Label = label, Sum = monthDonations.Sum(d => d.Amount), Count = monthDonations.Count });

                vm.NewMembersPerMonth.Add(new MonthlyPoint { Label = label, Count = members.Count(u => u.CreatedAt >= m && u.CreatedAt < next) });
            }

            vm.TopCategories = await Db.EventRegistrations
                .Where(r => r.Status == RegistrationStatus.Confirmed && r.Event != null)
                .GroupBy(r => r.Event!.Category)
                .Select(g => new CategoryPoint { Label = g.Key.ToString(), Count = g.Count() })
                .OrderByDescending(c => c.Count)
                .ToListAsync();

            return View(vm);
        }
    }
}
