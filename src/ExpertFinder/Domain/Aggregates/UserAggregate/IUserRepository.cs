namespace ExpertFinder.Domain.Aggregates.UserAggregate;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
}