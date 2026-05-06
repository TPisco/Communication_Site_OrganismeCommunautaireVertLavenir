using Microsoft.AspNetCore.Mvc;

namespace Communication_VertLavenir.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
