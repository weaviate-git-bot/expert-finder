using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL.Resolvers;

public class UserResolver
{
    public IQueryable<User> GetUsers([Service] ApplicationDbContext dbContext)
    {
        return dbContext.Users.OrderBy(x => x.FullName);
    }
}