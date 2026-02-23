
namespace SurvayBasket2026.Services
{
    public interface IPollService
    {
        Task<IEnumerable<PollResponse>> GetAllPollsAsync(CancellationToken cancellationToken = default);
        Task<PollResponse?> GetPollByIdAsync(int id , CancellationToken cancellationToken = default);
        Task<PollResponse> CreatePollAsync(PollRequest request, CancellationToken cancellationToken = default);
        Task<bool> UpdatePollAsync(int id, PollRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeletePollAsync(int id, CancellationToken cancellationToken = default);
    }
}
