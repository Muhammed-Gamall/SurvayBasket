
using FluentValidation;

namespace SurvayBasket2026.Contracts.Authentication
{
    public class AuthValidations : AbstractValidator<LoginRequest>
    {
        public AuthValidations()
        {
            RuleFor(a => a.Email)
                .NotEmpty().EmailAddress();

            RuleFor(a => a.Password)
              .NotEmpty();
        }

       
    }
}
