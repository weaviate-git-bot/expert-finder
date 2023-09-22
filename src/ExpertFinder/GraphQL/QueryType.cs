using ExpertFinder.GraphQL.Resolvers;

namespace ExpertFinder.GraphQL;

public class QueryType : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Field("articles").UsePaging().ResolveWith<ArticleResolver>(r => r.GetArticles(default!));
        descriptor.Field("users").UsePaging().ResolveWith<UserResolver>(r => r.GetUsers(default!));
    }
}