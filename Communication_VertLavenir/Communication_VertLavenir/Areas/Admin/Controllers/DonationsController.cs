using Communication_VertLavenir.Areas.Admin.Models;
using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Areas.Admin.Controllers
{
    public class DonationsController : AdminBaseController
    {
        public DonationsController(AppDbContext db) : base(db) { }

        public async Task<IActionResult> Index(DonationFilterViewModel filter)
        {
            var query = Db.Donations.AsQueryable();

            if (filter.From.HasValue) query = query.Where(d => d.CreatedAt >= filter.From.Value);
            if (filter.To.HasValue) query = query.Where(d => d.CreatedAt < filter.To.Value.AddDays(1));
            if (filter.MinAmount.HasValue) query = query.Where(d => d.Amount >= filter.MinAmount.Value);
            if (filter.Status.HasValue) query = query.Where(d => d.Status == filter.Status.Value);
            if (filter.Method.HasValue) query = query.Where(d => d.PaymentMethod == filter.Method.Value);

            filter.Results = await query.OrderByDescending(d => d.CreatedAt).ToListAsync();
            filter.Total = filter.Results.Where(d => d.Status == DonationStatus.Completed).Sum(d => d.Amount);

            return View(filter);
        }

        public async Task<IActionResult> Details(int id)
        {
            var donation = await Db.Donations.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
            if (donation == null) return NotFound();
            return View(donation);
        }
    }
}
