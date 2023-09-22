using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL.Users;

public class UserResolver
{
    public IQueryable<User> GetUsers(ApplicationDbContext dbContext)
    {
        return dbContext.Users.OrderBy(x => x.FullName);
    }
}