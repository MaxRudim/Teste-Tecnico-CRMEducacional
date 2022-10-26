using CourseApplications.Models;

namespace CourseApplications.Repository;

public interface ISubscriptionRepository
{
  public Task<Subscription> Add(Subscription subscription);
  public Task Delete(Guid id);
  public Task Update(Subscription subscription);
  public Task<Subscription?> Get(Guid SubscriptionId);
  public Task<IEnumerable<Subscription>> GetAll();
}