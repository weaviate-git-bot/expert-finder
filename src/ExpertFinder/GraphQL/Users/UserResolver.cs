using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Infrastructure.Persistence;

namespace ExpertFinder.GraphQL.Users;

public class UserResolver
{
    public IQueryable<User> GetUsers(IUserRepository userRepository)
    {
        return userRepository.GetUsers();
    }
}