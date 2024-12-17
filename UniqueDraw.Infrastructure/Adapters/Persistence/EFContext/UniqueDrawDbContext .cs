using Microsoft.EntityFrameworkCore;
using UniqueDraw.Domain.Entities.Base;
using UniqueDraw.Domain.Entities.UniqueDraw;

namespace UniqueDraw.Infrastructure.Adapters.Persistence.EFContext;

public class UniqueDrawDbContext(DbContextOptions<UniqueDrawDbContext> options) : DbContext(options)
{
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

        modelBuilder.Entity<AssignedNumber>()
            .HasOne(an => an.Client)
            .WithMany(c => c.AssignedNumbers)
            .HasForeignKey(an => an.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AssignedNumber>()
            .HasOne(an => an.User)
            .WithMany(u => u.AssignedNumbers)
            .HasForeignKey(an => an.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<AssignedNumber>()
            .HasOne(an => an.Raffle)
            .WithMany(r => r.AssignedNumbers)
            .HasForeignKey(an => an.RaffleId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Raffle>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Raffles)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Client)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(DomainEntity).IsAssignableFrom(entityType.ClrType))
                modelBuilder.Entity(entityType.Name)
                    .Property<DateTime>("CreatedOn").HasDefaultValueSql("GETDATE()");
        }
        base.OnModelCreating(modelBuilder);
    }
}
