
using FluentValidation;

namespace SurvayBasket2026.Contracts.Poll
{
    public class AuthValidations : AbstractValidator<PollRequest>
    {
        public AuthValidations()
        {
            RuleFor(poll => poll.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

        RuleFor(poll => poll.StartedAt).NotEmpty().GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Start date must be today or a future date.");

        RuleFor(poll => poll.EndAt).NotEmpty();

        RuleFor(x => x).Must(BeAValidDate)
                .WithMessage("end date must be bigger than start date");
        }

        private bool BeAValidDate(PollRequest poll)
        {
            return poll.EndAt > poll.StartedAt;
        }
    }
}
