using Microsoft.EntityFrameworkCore;
using CourseApplications.Models;

namespace CourseApplications.Repository;
public class CandidateRepository : ICandidateRepository
{
    protected readonly CourseContext _context;
    public CandidateRepository(CourseContext context)
    {
        _context = context;
    }

    public async Task<Candidate> Add(Candidate candidate)
    {
        _context.Add(candidate);

        await _context.SaveChangesAsync();

        return candidate;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.Candidates!.Include(e => e.Subscriptions).Single(p => p.CandidateId == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Candidate candidate)
    {
        _context.ChangeTracker.Clear();

        _context.Update(candidate);

        await _context.SaveChangesAsync();
    }   

    public async Task<Candidate?> Get(Guid CandidateId)
    {
        var candidate = await _context.Candidates!.AsNoTracking().FirstOrDefaultAsync(a => a.CandidateId == CandidateId);

        return candidate;
    }

    public async Task<Candidate?> GetByCpf(string cpf)
    {
        var candidate = await _context.Candidates!.AsNoTracking().FirstOrDefaultAsync(a => a.Cpf == cpf);

        return candidate;
    }

    public async Task<IEnumerable<Candidate>> GetAll()
    {
        var candidates = await _context.Candidates!.ToListAsync();

        return candidates;
    }

}