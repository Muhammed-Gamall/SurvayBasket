
namespace SurvayBasket2026.Context.EntitiesConfigrations
{
    public class QuestionConfigrations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasIndex(p =>new { p.PollId , p.Content }).IsUnique();
            builder.Property(p => p.Content).HasMaxLength(100).IsRequired();
        }
    }
}
