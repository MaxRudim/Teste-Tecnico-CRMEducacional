using Microsoft.EntityFrameworkCore;
using CourseApplications.Models;

namespace CourseApplications.Repository;
public class SubscriptionRepository : ISubscriptionRepository
{
    protected readonly CourseContext _context;
    public SubscriptionRepository(CourseContext context)
    {
        _context = context;
    }

    public async Task<Subscription> Add(Subscription subscription)
    {
        _context.Add(subscription);

        await _context.SaveChangesAsync();

        return subscription;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.Subscriptions!.Single(p => p.SubscriptionId == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Subscription subscription)
    {
        _context.ChangeTracker.Clear();

        _context.Update(subscription);

        await _context.SaveChangesAsync();
    }   

    public async Task<Subscription?> Get(Guid SubscriptionId)
    {
        var subscription = await _context.Subscriptions!.AsNoTracking().FirstOrDefaultAsync(a => a.SubscriptionId == SubscriptionId);

        return subscription;
    }

    public async Task<IEnumerable<Subscription>> GetAll()
    {
        var subscriptions = await _context.Subscriptions!.ToListAsync();

        return subscriptions;
    }

}