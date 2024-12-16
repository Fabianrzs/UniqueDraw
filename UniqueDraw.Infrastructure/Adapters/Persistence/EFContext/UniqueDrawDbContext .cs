using Microsoft.EntityFrameworkCore;
using UniqueDraw.Domain.Entities.Base;
using UniqueDraw.Domain.Entities.UniqueDraw;

namespace UniqueDraw.Infrastructure.Adapters.Persistence.EFContext
{
    public class UniqueDrawDbContext : DbContext
    {
        public UniqueDrawDbContext(DbContextOptions<UniqueDrawDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Raffle> Raffles { get; set; }
        public DbSet<AssignedNumber> AssignedNumbers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedOn")
                        .CurrentValue = DateTime.UtcNow;
                if (entry.State == EntityState.Modified)
                    entry.Property("LastModifiedOn")
                        .CurrentValue = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(DomainEntity).IsAssignableFrom(entityType.ClrType))
                    modelBuilder.Entity(entityType.Name)
                        .Property<DateTime>("CreatedOn").HasDefaultValueSql("GETDATE()");
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
