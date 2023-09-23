using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        descriptor.Ignore(x => x.Embedding);
        descriptor.Ignore(x => x.AuthorId);
    }
}