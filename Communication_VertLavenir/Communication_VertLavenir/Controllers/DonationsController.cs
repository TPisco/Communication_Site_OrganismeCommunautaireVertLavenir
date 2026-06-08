using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Communication_VertLavenir.Services;
using Communication_VertLavenir.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Controllers
{
    public class DonationsController : BaseController
    {
        private readonly IPaymentService _payment;
        private readonly IEmailService _email;
        private readonly ICurrentUserService _currentUser;

        public DonationsController(AppDbContext db, IPaymentService payment, IEmailService email, ICurrentUserService currentUser) : base(db)
        {
            _payment = payment;
            _email = email;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new DonationViewModel();
            if (_currentUser.IsAuthenticated && _currentUser.UserId is int uid)
            {
                var user = await Db.Users.FindAsync(uid);
                if (user != null)
                {
                    vm.FirstName = user.FirstName;
                    vm.LastName = user.LastName;
                    vm.Email = user.Email;
                    vm.Phone = user.Phone;
                }
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DonationViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var donation = new Donation
            {
                Amount = vm.Amount,
                Currency = "CAD",
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                Phone = vm.Phone,
                AddressLine1 = vm.AddressLine1,
                City = vm.City,
                PostalCode = vm.PostalCode,
                Province = vm.Province,
                PaymentMethod = vm.PaymentMethod,
                Status = DonationStatus.Pending,
                UserId = _currentUser.UserId,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _payment.ProcessAsync(donation);
            donation.Status = result.Success ? DonationStatus.Completed : DonationStatus.Failed;

            Db.Donations.Add(donation);
            await Db.SaveChangesAsync();

            if (!result.Success)
            {
                TempData["Error"] = "Le paiement a échoué. Veuillez réessayer. / Payment failed. Please try again.";
                return View(vm);
            }

            await _email.SendAsync(donation.Email, "Merci pour votre don / Thank you for your donation",
                $"Don de {donation.Amount:C} reçu.");

            return RedirectToAction(nameof(ThankYou), new { id = donation.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ThankYou(int id)
        {
            var donation = await Db.Donations.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (donation == null) return NotFound();
            return View(donation);
        }
    }
}
