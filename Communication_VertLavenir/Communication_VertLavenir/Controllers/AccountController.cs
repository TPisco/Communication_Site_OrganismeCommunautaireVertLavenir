using System.Security.Claims;
using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Communication_VertLavenir.Services;
using Communication_VertLavenir.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IPasswordHasherService _hasher;
        private readonly ICurrentUserService _currentUser;

        public AccountController(AppDbContext db, IPasswordHasherService hasher, ICurrentUserService currentUser) : base(db)
        {
            _hasher = hasher;
            _currentUser = currentUser;
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var email = vm.Email.Trim().ToLowerInvariant();
            if (await Db.Users.AnyAsync(u => u.Email == email))
            {
                ModelState.AddModelError(nameof(vm.Email), "Un compte existe déjà avec ce courriel.");
                return View(vm);
            }

            var user = new ApplicationUser
            {
                FirstName = vm.FirstName.Trim(),
                LastName = vm.LastName.Trim(),
                Email = email,
                PasswordHash = _hasher.Hash(vm.Password),
                Role = UserRole.Member,
                CreatedAt = DateTime.UtcNow
            };
            Db.Users.Add(user);
            await Db.SaveChangesAsync();

            await SignInUserAsync(user, true);
            TempData["Success"] = "Bienvenue ! Votre compte a été créé. / Welcome! Your account was created.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
            => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var email = vm.Email.Trim().ToLowerInvariant();
            var user = await Db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !_hasher.Verify(vm.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Courriel ou mot de passe invalide. / Invalid email or password.");
                return View(vm);
            }

            await SignInUserAsync(user, vm.RememberMe);

            if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                return Redirect(vm.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (_currentUser.UserId is not int uid) return Challenge();

            var user = await Db.Users.FindAsync(uid);
            if (user == null) return Challenge();

            var registrations = await Db.EventRegistrations
                .Include(r => r.Event)
                .Where(r => r.UserId == uid)
                .ToListAsync();

            var now = DateTime.Now;
            var vm = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UpcomingRegistrations = registrations.Where(r => r.Event != null && r.Event.StartDateTime >= now).OrderBy(r => r.Event!.StartDateTime).ToList(),
                PastRegistrations = registrations.Where(r => r.Event != null && r.Event.StartDateTime < now).OrderByDescending(r => r.Event!.StartDateTime).ToList(),
                Donations = await Db.Donations.Where(d => d.UserId == uid).OrderByDescending(d => d.CreatedAt).ToListAsync()
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel vm)
        {
            if (_currentUser.UserId is not int uid) return Challenge();
            var user = await Db.Users.FindAsync(uid);
            if (user == null) return Challenge();

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Profile));
            }

            user.FirstName = vm.FirstName.Trim();
            user.LastName = vm.LastName.Trim();
            user.Phone = vm.Phone;
            user.UpdatedAt = DateTime.UtcNow;
            await Db.SaveChangesAsync();

            await SignInUserAsync(user, true);
            TempData["Success"] = "Profil mis à jour. / Profile updated.";
            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        public IActionResult Denied() => View();

        private async Task SignInUserAsync(ApplicationUser user, bool persist)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Email),
                new("FullName", user.FullName),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = persist });
        }
    }
}
