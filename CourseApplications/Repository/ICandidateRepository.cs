using CourseApplications.Models;

namespace CourseApplications.Repository;

public interface ICandidateRepository
{
  public Task<Candidate> Add(Candidate candidate);
  public Task Delete(Guid id);
  public Task Update(Candidate candidate);
  public Task<Candidate?> Get(Guid CandidateId);
  public Task<IEnumerable<Candidate>> GetAll();
}