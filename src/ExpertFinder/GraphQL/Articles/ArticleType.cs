using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Services;
using ExpertFinder.Infrastructure.Persistence;

namespace ExpertFinder.GraphQL.Articles;

public class ArticleType : ObjectType<Article>
{
    protected override void Configure(IObjectTypeDescriptor<Article> descriptor)
    {
        descriptor.Name("Article");
        descriptor.Description("An article with interesting content");

        descriptor.Field("author").Resolve(context =>
        {
            var authorId = context.Parent<Article>().AuthorId;
            var dbContext = context.Services.GetRequiredService<ApplicationDbContext>();

            return dbContext.Users.Single(x => x.Id == authorId);
        });

        descriptor.Field("likes").Resolve(context =>
        {
            var articleId = context.Parent<Article>().Id;
            var dbContext = context.Services.GetRequiredService<ApplicationDbContext>();
            return dbContext.Likes.Count(x => x.ArticleId == articleId);
        });

        descriptor.Field("experts").Resolve(context =>
        {
            var searchEngine = context.Services.GetRequiredService<ISearchEngine>();
            var article = context.Parent<Article>();

            return searchEngine.FindExpertAsync(article);
        });

        descriptor.Ignore(x => x.Embedding);
        descriptor.Ignore(x => x.AuthorId);
        descriptor.Ignore(x => x.PendingDomainEvents);
    }
}