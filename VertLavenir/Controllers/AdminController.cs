using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertLavenir.Data;

namespace VertLavenir.Controllers;

[Authorize(Roles = "Administrateur")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.NombreArticles = await _context.Articles.CountAsync();
        ViewBag.NombreArticlesPublies = await _context.Articles.CountAsync(a => a.EstPublie);
        ViewBag.NombreEvenements = await _context.Evenements.CountAsync();
        ViewBag.NombreEvenementsActifs = await _context.Evenements.CountAsync(e => e.EstActif && e.DateDebut >= DateTime.Today);
        ViewBag.NombreMembres = await _context.Membres.CountAsync();
        ViewBag.NombreMembresActifs = await _context.Membres.CountAsync(m => m.EstActif);
        return View();
    }
}
