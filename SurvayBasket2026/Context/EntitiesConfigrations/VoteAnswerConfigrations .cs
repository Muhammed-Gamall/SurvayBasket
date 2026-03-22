
namespace SurvayBasket2026.Context.EntitiesConfigrations
{
    public class VoteAnswerConfigrations : IEntityTypeConfiguration<VoteAnswer>
    {
        public void Configure(EntityTypeBuilder<VoteAnswer> builder)
        {
            builder.HasIndex(p =>new { p.VoteId , p.QuestionId }).IsUnique();
            
        }
    }
}
