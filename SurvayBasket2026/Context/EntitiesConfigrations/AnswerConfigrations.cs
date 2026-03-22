
namespace SurvayBasket2026.Context.EntitiesConfigrations
{
    public class AnswerConfigrations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasIndex(p => new { p.QuestionId , p.Content }).IsUnique();
            builder.Property(p => p.Content).HasMaxLength(100).IsRequired();
        }

    }
}
