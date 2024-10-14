using Microsoft.EntityFrameworkCore;
using Rewardify.Core.Entities;

namespace Rewardify.Infrastructure.Data;

public class RewardifyDbContext(DbContextOptions<RewardifyDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<RewardTransaction> RewardTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PointsBalance).IsRequired();
        });

        modelBuilder.Entity<RewardTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);
            entity.Property(e => e.PurchaseAmount).IsRequired();
            entity.Property(e => e.PointsEarned).IsRequired();
            entity.Property(e => e.TransactionDate).IsRequired();
            entity.HasOne<Customer>()
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CustomerId);
        });
    }

    public static class RewardifyDbInitializer
    {
        public static void Initialize(RewardifyDbContext context)
        {
            context.Database.Migrate(); // Apply any pending migrations to the database.
        }
    }

}