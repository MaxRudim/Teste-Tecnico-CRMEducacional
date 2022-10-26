using CourseApplications.Models;

namespace CourseApplications.Repository;

public interface ICourseRepository
{
  public Task<Course> Add(Course course);
  public Task Delete(Guid id);
  public Task Update(Course course);
  public Task<Course?> Get(Guid courseId);
  public Task<IEnumerable<Course>> GetAll();
}