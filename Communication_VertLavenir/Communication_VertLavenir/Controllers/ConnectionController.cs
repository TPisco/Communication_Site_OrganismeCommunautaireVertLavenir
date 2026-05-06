using Microsoft.AspNetCore.Mvc;

namespace Communication_VertLavenir.Controllers
{
    public class ConnectionController : Controller
    {
        public IActionResult Index()
        {
            // Fournir une instance de modèle à la vue pour éviter les NullReference dans les tag-helpers (asp-for, validation).
            return View(new Models.utilisateur());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Models.utilisateur model)
        {
            // Si le modèle est invalide, réafficher la vue avec les erreurs de validation.
          
            // TODO: ajouter ici la logique d'authentification si nécessaire.

            // Après soumission réussie, rediriger vers Home/Index
            return RedirectToAction("Index", "Home");
        }
    }
}
