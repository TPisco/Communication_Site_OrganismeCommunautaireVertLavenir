using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Controllers
{
    // Loads shared site settings (footer contact + social links) into ViewBag for every public page.
    public abstract class BaseController : Controller
    {
        protected readonly AppDbContext Db;

        protected BaseController(AppDbContext db)
        {
            Db = db;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var settings = await Db.SiteSettings.AsNoTracking().ToDictionaryAsync(s => s.Key, s => s.Value);

            string? Get(string key) => settings.TryGetValue(key, out var v) ? v : null;

            ViewBag.ContactEmail = Get(SiteSettingKeys.ContactEmail);
            ViewBag.ContactPhone = Get(SiteSettingKeys.ContactPhone);
            ViewBag.ContactAddress = Get(SiteSettingKeys.ContactAddress);
            ViewBag.SocialFacebook = Get(SiteSettingKeys.SocialFacebook);
            ViewBag.SocialInstagram = Get(SiteSettingKeys.SocialInstagram);
            ViewBag.SocialLinkedIn = Get(SiteSettingKeys.SocialLinkedIn);

            await next();
        }
    }
}
