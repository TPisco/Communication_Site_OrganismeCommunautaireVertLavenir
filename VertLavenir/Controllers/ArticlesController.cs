using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertLavenir.Data;
using VertLavenir.Models;

namespace VertLavenir.Controllers;

[Authorize(Roles = "Administrateur")]
public class ArticlesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ArticlesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Articles
    public async Task<IActionResult> Index()
    {
        return View(await _context.Articles.OrderByDescending(a => a.DatePublication).ToListAsync());
    }

    // GET: Articles/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
        if (article == null)
            return NotFound();

        return View(article);
    }

    // GET: Articles/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Articles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titre,Contenu,DatePublication,Auteur,Categorie,EstPublie,ImageUrl")] Article article)
    {
        if (ModelState.IsValid)
        {
            _context.Add(article);
            await _context.SaveChangesAsync();
            TempData["Success"] = "L'article a été créé avec succès.";
            return RedirectToAction(nameof(Index));
        }
        return View(article);
    }

    // GET: Articles/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var article = await _context.Articles.FindAsync(id);
        if (article == null)
            return NotFound();

        return View(article);
    }

    // POST: Articles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Contenu,DatePublication,Auteur,Categorie,EstPublie,ImageUrl")] Article article)
    {
        if (id != article.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(article);
                await _context.SaveChangesAsync();
                TempData["Success"] = "L'article a été modifié avec succès.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.Id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(article);
    }

    // GET: Articles/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
        if (article == null)
            return NotFound();

        return View(article);
    }

    // POST: Articles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article != null)
            _context.Articles.Remove(article);

        await _context.SaveChangesAsync();
        TempData["Success"] = "L'article a été supprimé avec succès.";
        return RedirectToAction(nameof(Index));
    }

    private bool ArticleExists(int id)
    {
        return _context.Articles.Any(e => e.Id == id);
    }
}
