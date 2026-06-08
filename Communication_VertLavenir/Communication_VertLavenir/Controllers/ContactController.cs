using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Communication_VertLavenir.Services;
using Communication_VertLavenir.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IEmailService _email;

        public ContactController(AppDbContext db, IEmailService email) : base(db)
        {
            _email = email;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new ContactViewModel { OpeningHours = await GetHours() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactViewModel vm)
        {
            vm.OpeningHours = await GetHours();
            if (!ModelState.IsValid) return View(vm);

            var message = new ContactMessage
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Subject = vm.Subject,
                Message = vm.Message,
                Status = ContactStatus.New,
                CreatedAt = DateTime.UtcNow
            };
            Db.ContactMessages.Add(message);
            await Db.SaveChangesAsync();

            await _email.SendAsync("contact@vertlavenir.org", $"[Contact] {vm.Subject}", vm.Message);

            TempData["Success"] = "Merci ! Votre message a été envoyé. / Thank you! Your message has been sent.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<string?> GetHours()
        {
            var lang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var key = lang switch
            {
                "en" => SiteSettingKeys.OpeningHoursEn,
                "es" => SiteSettingKeys.OpeningHoursEs,
                _ => SiteSettingKeys.OpeningHoursFr
            };
            var settings = await Db.SiteSettings.AsNoTracking()
                .Where(x => x.Key == key || x.Key == SiteSettingKeys.OpeningHoursFr)
                .ToListAsync();
            return (settings.FirstOrDefault(x => x.Key == key) ?? settings.FirstOrDefault())?.Value;
        }
    }
}
