namespace ExpertFinder.Domain.Aggregates.UserAggregate;

public interface IUserRepository
{
    IQueryable<User> GetUsers();
    Task<User?> GetUserByIdAsync(Guid userId);
}