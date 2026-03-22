using Azure.Core;

namespace SurvayBasket2026.Services.Question
{
    public class QuestionService(ApplicationDbContext context) : IQuestionService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<QuestionResponse>?> GetAllQuestionAsync(int pollId, CancellationToken cancellation = default)
        {
            var IsPollExist = await _context.Polls.AnyAsync(p => p.Id == pollId, cancellation);
            if (!IsPollExist)
                return null;

            var questions = await _context.Questions.Where(q => q.PollId == pollId)
                .Include(x => x.Answers).AsNoTracking()
                .Select(q => new QuestionResponse
                (
                     q.Id,
                     q.Content,
                     q.Answers.Where(x => x.IsActive == true).Select(a =>new AnswerResponse (a.Id ,a.Content) )
                ))
                .ToListAsync(cancellation);
            return questions;
        }

        public async Task<QuestionResponse?> GetQuestionAsync(int pollId, int questionId, CancellationToken cancellation = default)
        {
            var question = await _context.Questions
                .Where(q => q.Id == questionId && q.PollId == pollId)
                .Include(x => x.Answers).AsNoTracking()
                .Select(q => new QuestionResponse
                (
                     q.Id,
                     q.Content,
                     q.Answers.Where(x=>x.IsActive == true).Select(a => new AnswerResponse(a.Id, a.Content))
                ))
                .SingleOrDefaultAsync(cancellation);

            return question;
        }

        public async Task<QuestionResponse?> CreateQuestionAsync(int pollId, QuestionRequest request, CancellationToken cancellation = default)

        {
            var IsPollExist = await _context.Polls.AnyAsync(p => p.Id == pollId, cancellation);
            if (!IsPollExist) 
                return null;

            var IsQuestionExist = await _context.Questions.AnyAsync(q => q.Content == request.Content && q.PollId == pollId, cancellation);
            if (IsQuestionExist)
                return null;

            var question = request.Adapt<Entities.Question>();
            question.PollId = pollId;
           
            await _context.Questions.AddAsync(question , cancellation);
            await _context.SaveChangesAsync(cancellation);

             return question.Adapt<QuestionResponse>();
        }
       
        public async Task<bool> UpdateAsync(int pollId, int questionId, QuestionRequest request, CancellationToken cancellation = default)
        {
            var question =await _context.Questions.Include(x=>x.Answers)
                .SingleOrDefaultAsync(q => q.Id == questionId && q.PollId == pollId);

              if (question is null)
                return false;

            var IsItHaveSameContent = await _context.Questions.AnyAsync(q => q.Id != questionId &&
                   q.PollId == pollId && q.Content == request.Content);
            if (IsItHaveSameContent)
                return false;

            question.Content = request.Content;

            //current answers
            var currentAnswers = question.Answers.Select(x=>x.Content).ToList();
            
            //Adding new answers
            var newAnswers = request.Answers.Except(currentAnswers).ToList();

            newAnswers.ForEach(a =>
            {
                question.Answers.Add(new Answer { Content = a });
            });

            question.Answers.ToList().ForEach( answer =>
            {
                answer.IsActive = request.Answers.Contains(answer.Content);
             });
            await _context.SaveChangesAsync(cancellation);
            return true;

        }
        public async Task<bool> ToggleAsync(int pollId, int questionId, CancellationToken cancellation = default)
        {
            var question =await _context.Questions.SingleOrDefaultAsync(q => q.Id == questionId && q.PollId == pollId);

            if (question is null)
                return false;

            question.IsActive = !question.IsActive;
            await _context.SaveChangesAsync(cancellation);

            return true;

        }

       
    }
}
