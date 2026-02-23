
namespace SurvayBasket2026.Context.EntitiesConfigrations
{
    public class UserConfigrations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.OwnsMany(x=>x.RefreshTokens)
                .ToTable("RefreshTokens").WithOwner().HasForeignKey("UserId");

            builder.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(30).IsRequired();    
        }
    }
}
