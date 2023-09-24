using ExpertFinder.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _applicationDbContext.Users.SingleAsync(
            x => x.Id == userId);
    }

    public IQueryable<User> GetUsers()
    {
        return _applicationDbContext.Users.OrderBy(x => x.FullName);
    }
}