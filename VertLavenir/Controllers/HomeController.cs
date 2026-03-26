using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VertLavenir.Data;
using VertLavenir.Models;

namespace VertLavenir.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _context.Articles
            .Where(a => a.EstPublie)
            .OrderByDescending(a => a.DatePublication)
            .Take(3)
            .ToListAsync();

        var evenements = await _context.Evenements
            .Where(e => e.EstActif && e.DateDebut >= DateTime.Today)
            .OrderBy(e => e.DateDebut)
            .Take(3)
            .ToListAsync();

        ViewBag.Articles = articles;
        ViewBag.Evenements = evenements;
        return View();
    }

    public IActionResult APropos()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

