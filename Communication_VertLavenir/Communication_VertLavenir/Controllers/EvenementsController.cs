using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Communication_VertLavenir.Models;
using Models.data;

namespace Communication_VertLavenir.Controllers
{
    public class EvenementsController : Controller
    {
        private readonly AppDbContext _context;

        public EvenementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Evenements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Evenements.ToListAsync());
        }

        // GET: Evenements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evenement = await _context.Evenements
                .FirstOrDefaultAsync(m => m.id == id);

            if (evenement == null) return NotFound();

            return View(evenement);
        }

        // GET: Evenements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Evenements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(evenement evenement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evenement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evenement);
        }

        // GET: Evenements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evenement = await _context.Evenements.FindAsync(id);
            if (evenement == null) return NotFound();

            return View(evenement);
        }

        // POST: Evenements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, evenement evenement)
        {
            if (id != evenement.id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evenement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Evenements.Any(e => e.id == evenement.id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(evenement);
        }

        // GET: Evenements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evenement = await _context.Evenements
                .FirstOrDefaultAsync(m => m.id == id);

            if (evenement == null) return NotFound();

            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evenement = await _context.Evenements.FindAsync(id);
            _context.Evenements.Remove(evenement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}