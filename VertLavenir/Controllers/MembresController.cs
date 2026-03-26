using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertLavenir.Data;
using VertLavenir.Models;

namespace VertLavenir.Controllers;

[Authorize(Roles = "Administrateur")]
public class MembresController : Controller
{
    private readonly ApplicationDbContext _context;

    public MembresController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Membres
    public async Task<IActionResult> Index()
    {
        return View(await _context.Membres.OrderBy(m => m.Nom).ThenBy(m => m.Prenom).ToListAsync());
    }

    // GET: Membres/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var membre = await _context.Membres.FirstOrDefaultAsync(m => m.Id == id);
        if (membre == null)
            return NotFound();

        return View(membre);
    }

    // GET: Membres/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Membres/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Prenom,Nom,Courriel,Telephone,DateAdhesion,EstActif,Role")] Membre membre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(membre);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Le membre a été ajouté avec succès.";
            return RedirectToAction(nameof(Index));
        }
        return View(membre);
    }

    // GET: Membres/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var membre = await _context.Membres.FindAsync(id);
        if (membre == null)
            return NotFound();

        return View(membre);
    }

    // POST: Membres/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Prenom,Nom,Courriel,Telephone,DateAdhesion,EstActif,Role")] Membre membre)
    {
        if (id != membre.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(membre);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Le membre a été modifié avec succès.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembreExists(membre.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(membre);
    }

    // GET: Membres/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var membre = await _context.Membres.FirstOrDefaultAsync(m => m.Id == id);
        if (membre == null)
            return NotFound();

        return View(membre);
    }

    // POST: Membres/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var membre = await _context.Membres.FindAsync(id);
        if (membre != null)
            _context.Membres.Remove(membre);

        await _context.SaveChangesAsync();
        TempData["Success"] = "Le membre a été supprimé avec succès.";
        return RedirectToAction(nameof(Index));
    }

    private bool MembreExists(int id)
    {
        return _context.Membres.Any(e => e.Id == id);
    }
}
