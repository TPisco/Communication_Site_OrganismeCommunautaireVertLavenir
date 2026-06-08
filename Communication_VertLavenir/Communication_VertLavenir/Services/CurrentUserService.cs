using System.Security.Claims;

namespace Communication_VertLavenir.Services
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        int? UserId { get; }
        string? Email { get; }
        string? FullName { get; }
        string? Role { get; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private ClaimsPrincipal? User => _accessor.HttpContext?.User;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public int? UserId
        {
            get
            {
                var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.TryParse(value, out var id) ? id : null;
            }
        }

        public string? Email => User?.FindFirstValue(ClaimTypes.Email);

        public string? FullName => User?.FindFirstValue("FullName");

        public string? Role => User?.FindFirstValue(ClaimTypes.Role);
    }
}
