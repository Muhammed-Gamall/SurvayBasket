namespace SurvayBasket2026.Authentication
{
    public interface IJwtProvider
    {
        (string Token , int ExpireIn) GenerateToken(ApplicationUser user);
        string? ValidateToken(string token);
    }
}
