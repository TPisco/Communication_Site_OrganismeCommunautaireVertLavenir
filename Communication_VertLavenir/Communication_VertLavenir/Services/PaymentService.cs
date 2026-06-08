using Communication_VertLavenir.Models;

namespace Communication_VertLavenir.Services
{
    public record PaymentResult(bool Success, string? TransactionId, string? Error);

    public interface IPaymentService
    {
        Task<PaymentResult> ProcessAsync(Donation donation);
    }

    // Simulated payment gateway. Always succeeds (no real processing).
    public class FakePaymentService : IPaymentService
    {
        public Task<PaymentResult> ProcessAsync(Donation donation)
        {
            var transactionId = $"SIM-{Guid.NewGuid():N}".Substring(0, 16).ToUpperInvariant();
            return Task.FromResult(new PaymentResult(true, transactionId, null));
        }
    }
}
