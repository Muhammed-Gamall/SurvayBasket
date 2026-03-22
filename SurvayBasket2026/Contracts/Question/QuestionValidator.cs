namespace SurvayBasket2026.Contracts.Question
{
    public class QuestionValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionValidator()
        {
            RuleFor(p => p.Content).NotEmpty().Length(5 ,100);

            RuleFor(p => p.Answers).NotNull().WithMessage("Question Should have at least 2 Answers");

            RuleFor(p => p.Answers)
                .Must(x=>x.Count>1).WithMessage("Question Should have at least 2 Answers")
                .When(x=>x.Answers !=null);

            RuleFor(x=>x.Answers)
                .Must(x =>x.Distinct().Count() == x.Count).WithMessage("Answers should be unique")
                .When(x=>x.Answers !=null);
            ;
        }
    }
} 
