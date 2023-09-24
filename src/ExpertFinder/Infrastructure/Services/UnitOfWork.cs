using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Shared;

namespace ExpertFinder.Infrastructure.Services;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}