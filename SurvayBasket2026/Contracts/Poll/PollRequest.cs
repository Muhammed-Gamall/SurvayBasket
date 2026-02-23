namespace SurvayBasket2026.Contracts.Poll
{
    public record PollRequest(
        string Title,
        string Summary,
        bool IsPublished,
        DateOnly StartedAt,
        DateOnly EndAt
    );
   
}
