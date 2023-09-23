using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Infrastructure.Persistence;

namespace ExpertFinder.GraphQL.Users;

public class UserResolver
{
    public IQueryable<User> GetUsers(ApplicationDbContext dbContext)
    {
        return dbContext.Users.OrderBy(x => x.FullName);
    }
}