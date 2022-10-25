using Microsoft.EntityFrameworkCore;
using CourseApplications.Models;

namespace CourseApplications.Repository;
public class CourseContext : DbContext
{
    public DbSet<Candidate>? Candidates { get; set; }
    public DbSet<Course>? Courses { get; set; }
    public DbSet<Subscription>? Subscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=crm-teste;User=SA;Password=SenhaSegura123.");
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
       mb.Entity<Subscription>().HasKey(sub => new { sub.CandidateId, sub.CourseId });
        // mb.Entity<Subscription>().HasOne(a => a.Course).WithOne(a => a.Subscription).HasForeignKey<Course>(a => a.CourseId);
        // mb.Entity<Course>().HasOne(a => a.Subscription).WithOne(a => a.Course).HasForeignKey<Course>(a => a.SubscriptionId);
        // mb.Entity<Subscription>().HasOne(a => a.Candidate).WithMany(a => a.Subscriptions).HasForeignKey(a => a.CandidateId);
        // mb.Entity<Candidate>().HasMany(a => a.Subscriptions).WithOne(a => a.Candidate);
        
    }
}