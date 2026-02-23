
using System.Security.Cryptography;

namespace SurvayBasket2026.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager , IJwtProvider jwtProvider) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly int _tokenExpiryDays = 14;

        public async Task<AuthResponse?> GetTokenAsync(string Email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null) 
                return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                return null;
          
            var (token, expireIn) = _jwtProvider.GenerateToken(user);

            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_tokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiry
            }); 
            await _userManager.UpdateAsync(user);

            return new AuthResponse(user.Id , user.Email! , user.FirstName , user.LastName , token, expireIn , refreshToken, refreshTokenExpiry);
        }

        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
           var userId = _jwtProvider.ValidateToken(token);
              if (userId == null)
                 return null;

          var user = await _userManager.FindByIdAsync(userId);
             if (user == null)
               return null;

             var existingRefreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken && rt.IsActive);
                if (existingRefreshToken == null)
                    return null;
                
                existingRefreshToken.RevokedOn = DateTime.UtcNow;

            var (newToken, expireIn) = _jwtProvider.GenerateToken(user);

            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_tokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiry,
                
            });
            await _userManager.UpdateAsync(user);

            return new AuthResponse(user.Id, user.Email!, user.FirstName, user.LastName, newToken, expireIn, newRefreshToken, refreshTokenExpiry);

        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        }


    }
}
