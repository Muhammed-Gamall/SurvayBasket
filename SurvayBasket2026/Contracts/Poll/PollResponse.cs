namespace SurvayBasket2026.Contracts.Poll
{
    public record PollResponse
    (
        int Id,
        string Title,
        string Summary,
        bool IsPublished,
        DateOnly StartedAt,
        DateOnly EndAt,
        string CreatedById ,
        DateTime CreatedOn,
        string? UpdatedById,
        DateTime? UpdatedOn 

    );
}
