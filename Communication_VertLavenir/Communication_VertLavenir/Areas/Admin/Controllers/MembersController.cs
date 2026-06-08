using Communication_VertLavenir.Areas.Admin.Models;
using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Areas.Admin.Controllers
{
    public class MembersController : AdminBaseController
    {
        public MembersController(AppDbContext db) : base(db) { }

        public async Task<IActionResult> Index(string? search)
        {
            var query = Db.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(u => u.Email.Contains(search) || u.FirstName.Contains(search) || u.LastName.Contains(search));

            ViewBag.Search = search;
            ViewBag.Roles = Enum.GetValues<UserRole>();
            return View(await query.OrderBy(u => u.LastName).ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await Db.Users.FindAsync(id);
            if (user == null) return NotFound();

            var vm = new MemberDetailViewModel
            {
                User = user,
                Registrations = await Db.EventRegistrations.Include(r => r.Event).Where(r => r.UserId == id).ToListAsync(),
                Donations = await Db.Donations.Where(d => d.UserId == id).OrderByDescending(d => d.CreatedAt).ToListAsync()
            };
            return View(vm);
        }

        // Only full admins may change roles.
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(int id, UserRole role)
        {
            var user = await Db.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Role = role;
            user.UpdatedAt = DateTime.UtcNow;
            await Db.SaveChangesAsync();
            TempData["Success"] = $"Rôle mis à jour : {user.FullName} → {role}.";
            return RedirectToAction(nameof(Index));
        }
    }
}
