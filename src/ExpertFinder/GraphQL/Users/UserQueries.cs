using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Infrastructure.Persistence;

namespace ExpertFinder.GraphQL.Users;

[ExtendObjectType(OperationTypeNames.Query)]
public class UserQueries
{
    [UsePaging]
    public IQueryable<User> GetUsers([Service] IUserRepository userRepository)
    {
        return userRepository.GetUsers();
    }
}