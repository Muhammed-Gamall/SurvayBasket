namespace SurvayBasket2026.Entities
{
    public sealed class VoteAnswer
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }


        public Answer answer { get; set; } = default!;
        public Question question { get; set; } = default!;
        public Vote Vote { get; set; } = default!;


    }
}
