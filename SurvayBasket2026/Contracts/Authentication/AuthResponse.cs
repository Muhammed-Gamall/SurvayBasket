namespace SurvayBasket2026.Contracts.Authentication
{
    public record AuthResponse
   (
     string Id ,
      string Email,
       string FirstName,
         string LastName,
         string Token,
         int TokenExpiration,

        string RefreshToken,
        DateTime RefreshTokenExpiration
   );
}
