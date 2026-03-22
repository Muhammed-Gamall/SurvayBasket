using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SurvayBasket2026.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IHttpContextAccessor httpContextAccessor)
        
        : IdentityDbContext<ApplicationUser>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Poll>Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteAnswer> VoteAnswers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

           var CascadeFKs= modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

            foreach (var fk in CascadeFKs)
               fk.DeleteBehavior = DeleteBehavior.Restrict;
           

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();

            foreach (var entry in entries)
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedById).CurrentValue = userId!;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedById).CurrentValue = userId;
                    entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }

    }
}
   