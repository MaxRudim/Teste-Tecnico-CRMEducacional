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
    }
}