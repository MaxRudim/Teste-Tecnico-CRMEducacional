using Microsoft.EntityFrameworkCore;
using CourseApplications.Models;

namespace CourseApplications.Repository
{
    public interface ICourseContext
    {
    public DbSet<Candidate>? Candidates { get; set; }
    public DbSet<Course>? Courses { get; set; }
    public DbSet<Subscription>? Subscriptions { get; set; }

    public int SaveChanges();

    }
}
