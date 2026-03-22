namespace SurvayBasket2026.Contracts.Question
{
    public record QuestionResponse
    (
        int Id,
        string Content,
       IEnumerable<AnswerResponse> Answers
    );
}
