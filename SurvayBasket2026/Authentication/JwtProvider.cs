
namespace SurvayBasket2026.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;


        public (string Token, int ExpireIn) GenerateToken(ApplicationUser user)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            };
          //  var JwtSettings = _configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>(); 

            var symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.key!));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var expireIn = _jwtOptions!.expiryMinutes;   

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtOptions!.issuer,
                audience: _jwtOptions.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireIn),
                signingCredentials: signingCredentials
            );

            return (new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), expireIn);
        }

        public string? ValidateToken(string token) 
        {

            var Handler = new JwtSecurityTokenHandler();
            var symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.key!));

            try
            {
                Handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
                return userId;
            }
            catch
            {
                // Token validation failed
                return null;
            }

        }
    }
}
