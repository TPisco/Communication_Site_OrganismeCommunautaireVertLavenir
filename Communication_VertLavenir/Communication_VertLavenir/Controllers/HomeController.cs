using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Communication_VertLavenir.Services;
using Communication_VertLavenir.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(AppDbContext db) : base(db) { }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                Mission = await GetMission(),
                Metrics = await Db.ImpactMetrics.OrderBy(m => m.DisplayOrder).ToListAsync(),
                Values = await Db.OrgValues.OrderBy(v => v.DisplayOrder).ToListAsync(),
                KeyActions = await Db.KeyActions.OrderBy(a => a.DisplayOrder).ToListAsync()
            };
            return View(vm);
        }

        public async Task<IActionResult> About()
        {
            var vm = new AboutViewModel
            {
                Mission = await GetMission(),
                Values = await Db.OrgValues.OrderBy(v => v.DisplayOrder).ToListAsync(),
                Metrics = await Db.ImpactMetrics.OrderBy(m => m.DisplayOrder).ToListAsync(),
                KeyActions = await Db.KeyActions.OrderBy(a => a.DisplayOrder).ToListAsync()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult SetLanguage(string culture, string? returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true });

            return LocalRedirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }

        [ResponseCache(Duration = 0, NoStore = true)]
        public IActionResult Error() => View();

        private async Task<string> GetMission()
        {
            var lang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var key = lang switch
            {
                "en" => SiteSettingKeys.MissionEn,
                "es" => SiteSettingKeys.MissionEs,
                _ => SiteSettingKeys.MissionFr
            };
            var settings = await Db.SiteSettings.AsNoTracking()
                .Where(x => x.Key == key || x.Key == SiteSettingKeys.MissionFr)
                .ToListAsync();
            var s = settings.FirstOrDefault(x => x.Key == key) ?? settings.FirstOrDefault();
            return s?.Value ?? string.Empty;
        }
    }
}
