using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL.Users;

[ExtendObjectType(OperationTypeNames.Query)]
public class UserQueries
{
    [UsePaging]
    public IQueryable<User> GetUsers([Service]ApplicationDbContext dbContext)
    {
        return dbContext.Users.OrderBy(x => x.FullName);
    }
}