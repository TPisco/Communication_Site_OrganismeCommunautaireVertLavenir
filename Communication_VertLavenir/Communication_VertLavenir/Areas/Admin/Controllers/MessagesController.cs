using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Areas.Admin.Controllers
{
    public class MessagesController : AdminBaseController
    {
        public MessagesController(AppDbContext db) : base(db) { }

        public async Task<IActionResult> Index()
        {
            return View(await Db.ContactMessages.OrderByDescending(m => m.CreatedAt).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStatus(int id, ContactStatus status)
        {
            var msg = await Db.ContactMessages.FindAsync(id);
            if (msg != null)
            {
                msg.Status = status;
                await Db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
