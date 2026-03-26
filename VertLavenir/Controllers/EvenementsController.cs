using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertLavenir.Data;
using VertLavenir.Models;

namespace VertLavenir.Controllers;

[Authorize(Roles = "Administrateur")]
public class EvenementsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EvenementsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Evenements
    public async Task<IActionResult> Index()
    {
        return View(await _context.Evenements.OrderByDescending(e => e.DateDebut).ToListAsync());
    }

    // GET: Evenements/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var evenement = await _context.Evenements.FirstOrDefaultAsync(m => m.Id == id);
        if (evenement == null)
            return NotFound();

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
    public async Task<IActionResult> Create([Bind("Id,Titre,Description,DateDebut,DateFin,Lieu,NombreMaxParticipants,EstActif")] Evenement evenement)
    {
        if (ModelState.IsValid)
        {
            _context.Add(evenement);
            await _context.SaveChangesAsync();
            TempData["Success"] = "L'événement a été créé avec succès.";
            return RedirectToAction(nameof(Index));
        }
        return View(evenement);
    }

    // GET: Evenements/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var evenement = await _context.Evenements.FindAsync(id);
        if (evenement == null)
            return NotFound();

        return View(evenement);
    }

    // POST: Evenements/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,DateDebut,DateFin,Lieu,NombreMaxParticipants,EstActif")] Evenement evenement)
    {
        if (id != evenement.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(evenement);
                await _context.SaveChangesAsync();
                TempData["Success"] = "L'événement a été modifié avec succès.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementExists(evenement.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(evenement);
    }

    // GET: Evenements/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var evenement = await _context.Evenements.FirstOrDefaultAsync(m => m.Id == id);
        if (evenement == null)
            return NotFound();

        return View(evenement);
    }

    // POST: Evenements/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var evenement = await _context.Evenements.FindAsync(id);
        if (evenement != null)
            _context.Evenements.Remove(evenement);

        await _context.SaveChangesAsync();
        TempData["Success"] = "L'événement a été supprimé avec succès.";
        return RedirectToAction(nameof(Index));
    }

    private bool EvenementExists(int id)
    {
        return _context.Evenements.Any(e => e.Id == id);
    }
}
