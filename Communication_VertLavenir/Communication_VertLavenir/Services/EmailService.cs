namespace Communication_VertLavenir.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }

    // Stub implementation — logs instead of sending. Replace with SMTP/provider later.
    public class NoOpEmailService : IEmailService
    {
        private readonly ILogger<NoOpEmailService> _logger;

        public NoOpEmailService(ILogger<NoOpEmailService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string to, string subject, string body)
        {
            _logger.LogInformation("[Email stub] To: {To} | Subject: {Subject}", to, subject);
            return Task.CompletedTask;
        }
    }
}
