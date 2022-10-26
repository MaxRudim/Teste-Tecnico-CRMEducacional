using Microsoft.EntityFrameworkCore;
using CourseApplications.Models;

namespace CourseApplications.Repository;
public class CourseRepository : ICourseRepository
{
    protected readonly CourseContext _context;
    public CourseRepository(CourseContext context)
    {
        _context = context;
    }

    public async Task<Course> Add(Course course)
    {
        _context.Add(course);

        await _context.SaveChangesAsync();

        return course;
    }
    public async Task Delete(Guid id)
    {
        var result = _context.Courses!.Single(p => p.CourseId == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Course course)
    {
        _context.ChangeTracker.Clear();

        _context.Update(course);

        await _context.SaveChangesAsync();
    }   

    public async Task<Course?> Get(Guid CourseId)
    {
        var course = await _context.Courses!.AsNoTracking().FirstOrDefaultAsync(a => a.CourseId == CourseId);

        return course;
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        var courses = await _context.Courses!.ToListAsync();

        return courses;
    }

}