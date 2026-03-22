
namespace SurvayBasket2026.Context.EntitiesConfigrations
{
    public class VoteConfigrations : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasIndex(p =>new { p.PollId , p.UserId }).IsUnique();
        }
    }
}
