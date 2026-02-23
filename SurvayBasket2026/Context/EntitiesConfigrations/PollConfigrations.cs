
namespace SurvayBasket2026.Context.EntitiesConfigrations
{
    public class PollConfigrations : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasIndex(p => p.Title).IsUnique();
            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
        }
    }
}
