using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.data;
using Models;
using Communication_VertLavenir.Views.ViewModels;

namespace Communication_VertLavenir.Controllers
{
    public class StatsController : Controller
    {
        private readonly AppDbContext _context;

        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<paiement> listPaiement = await _context.Paiements.ToListAsync();

            var totalUtilisateurs = await _context.Utilisateurs.CountAsync();
            var totalPaiements = listPaiement.Count;
            var totalEvenements = await _context.Evenements.CountAsync();
            int totalMontantRecolte = 0;

            foreach(paiement p in listPaiement)
            {
                totalMontantRecolte += p.montant;
            }

            var utilisateursParRole = await _context.Utilisateurs
                .GroupBy(u => u.role)
                .Select(g => new
                {
                    Role = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var evenementsParType = await _context.Evenements
                .GroupBy(e => e.type)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var model = new StatsViewModel
            {
                TotalUtilisateurs = totalUtilisateurs,
                TotalPaiements = totalPaiements,
                TotalEvenements = totalEvenements,
                TotalMontantRecolte = totalMontantRecolte
            };

            foreach (role r in Enum.GetValues(typeof(role)))
            {
                model.UtilisateursParRole[r] = utilisateursParRole
                    .FirstOrDefault(x => x.Role == r)?.Count ?? 0;
            }

            foreach (typeEvent t in Enum.GetValues(typeof(typeEvent)))
            {
                model.EvenementsParType[t] = evenementsParType
                    .FirstOrDefault(x => x.Type == t)?.Count ?? 0;
            }

            return View(model);
        }

    }

}

