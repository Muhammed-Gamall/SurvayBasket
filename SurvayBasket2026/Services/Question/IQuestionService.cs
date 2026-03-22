
namespace SurvayBasket2026.Services.Question
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionResponse>?> GetAllQuestionAsync(int pollId, CancellationToken cancellation = default);
        Task<QuestionResponse?> GetQuestionAsync(int pollId, int questionId, CancellationToken cancellation = default);
        Task<QuestionResponse?> CreateQuestionAsync(int pollId, QuestionRequest request , CancellationToken cancellation = default);
        Task<bool> UpdateAsync(int pollId, int questionId, QuestionRequest request, CancellationToken cancellation = default);
        Task<bool> ToggleAsync(int pollId, int questionId, CancellationToken cancellation = default);

    }
}
