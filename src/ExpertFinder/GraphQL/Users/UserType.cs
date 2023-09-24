using ExpertFinder.Domain.Aggregates.UserAggregate;

namespace ExpertFinder.GraphQL.Users;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Ignore(x => x.ExpertiseEmbedding);
        descriptor.Ignore(x => x.PendingDomainEvents);
    }
}