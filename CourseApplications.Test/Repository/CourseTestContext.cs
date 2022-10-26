namespace CourseApplications.Test.Repository;

using Microsoft.EntityFrameworkCore;
using CourseApplications.Models;
using Microsoft.Extensions.DependencyInjection;
using CourseApplications.Repository;


public class CourseTestContext : CourseContext
{
    public DbSet<Candidate> Candidate1 { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        optionsBuilder.UseInMemoryDatabase("Candidate1").UseInternalServiceProvider(serviceProvider);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
          .HasKey(i => i.CandidateId);
        modelBuilder.Entity<Course>()
          .HasKey(i => i.CourseId);
        modelBuilder.Entity<Subscription>()
          .HasKey(i => i.SubscriptionId);

        modelBuilder.Entity<Subscription>()
          .HasOne(i => i.Candidate)
          .WithMany(i => i.Subscriptions)
          .HasForeignKey(b => b.CandidateId)
          .OnDelete(DeleteBehavior.Cascade);
    }

}