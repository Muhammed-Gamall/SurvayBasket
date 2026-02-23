namespace SurvayBasket2026.Contracts.Authentication
{
    public class RefreshTokenValidations : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidations()
        {
            RuleFor(r => r.Token).NotEmpty();

            RuleFor(r=>r.RefreshToken).NotEmpty();

        }
    }
}
