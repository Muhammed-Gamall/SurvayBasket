
namespace SurvayBasket2026.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> GetTokenAsync(string username, string password , CancellationToken cancellationToken = default);
        Task<AuthResponse?> GetRefreshTokenAsync(string  token , string refreshToken, CancellationToken cancellationToken = default);
    }
}
