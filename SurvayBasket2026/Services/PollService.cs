
using Azure.Core;
using SurvayBasket2026.Entities;

namespace SurvayBasket2026.Services
{
    public class PollService(ApplicationDbContext context) : IPollService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<PollResponse>> GetAllPollsAsync(CancellationToken cancellationToken)
        {
          var polls = await _context.Polls.AsNoTracking()
                .ProjectToType<PollResponse>()
                .ToListAsync(cancellationToken);
            //var response = polls.Adapt<IEnumerable<PollResponse>>();
            return polls;
        }

        public async Task<PollResponse?> GetPollByIdAsync(int id, CancellationToken cancellationToken)
        {
            var polls = _context.Polls.AsNoTracking()
                .ProjectToType<PollResponse>()
                .FirstOrDefault(x => x.Id == id);
            //var response =  polls.Adapt<PollResponse>();
            return polls;
        }

        public async Task<PollResponse> CreatePollAsync(PollRequest request, CancellationToken cancellationToken)
        {
            var poll = request.Adapt<Poll>();
            await _context.Polls.AddAsync(poll, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var response = poll.Adapt<PollResponse>();
            return response;
        }

        public async Task<bool> UpdatePollAsync(int id, PollRequest request, CancellationToken cancellationToken)
        {
            var existingPoll = await GetPollByIdAsync(id, cancellationToken);
            if (existingPoll == null)
                return false;

            var poll = request.Adapt(existingPoll);
            var entity = poll.Adapt<Poll>();

            _context.Polls.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeletePollAsync(int id, CancellationToken cancellationToken)
        {
            var existingPoll = await GetPollByIdAsync(id, cancellationToken);
            if (existingPoll == null)
                return false;
            var poll = existingPoll.Adapt<Poll>();
            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }



    }
}
