using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Communication_VertLavenir.Models;
using Models.data; // Corrected namespace for AppDbContext

namespace Communication_VertLavenir.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly AppDbContext _context;

        public UtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch users, excluding admins (fixed 'Utilisateurs' property name)
            var users = await _context.Utilisateurs
                .Where(u => u.role != role.admin)
                .ToListAsync();

            // Generate dropdown lists from the role enum (excluding admin)
            ViewBag.AvailableRoles = Enum.GetValues(typeof(role))
                .Cast<role>()
                .Where(r => r != role.admin)
                .Select(r => new SelectListItem
                {
                    Value = r.ToString(),
                    Text = r.ToString()
                })
                .ToList();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(int userId, role newRole)
        {
            // Fixed 'Utilisateurs' property name
            var user = await _context.Utilisateurs.FindAsync(userId);
            
            if (user != null)
            {
                user.role = newRole;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
