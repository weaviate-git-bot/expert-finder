using ExpertFinder.Data;
using ExpertFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.GraphQL.Articles;

public class ArticleType : ObjectType<Article>
{
    protected override void Configure(IObjectTypeDescriptor<Article> descriptor)
    {
        descriptor.Name("Article");
        descriptor.Description("An article with interesting content");

        descriptor.Field(x => x.Author).Resolve(context =>
        {
            var articleId = context.Parent<Article>().Id;
            var dbContext = context.Services.GetRequiredService<ApplicationDbContext>();

            return dbContext.Articles.Include(x => x.Author).Single(x => x.Id == articleId).Author;
        });

        descriptor.Field("likes").Resolve(context =>
        {
            var articleId = context.Parent<Article>().Id;
            var dbContext = context.Services.GetRequiredService<ApplicationDbContext>();
            return dbContext.Likes.Count(x => x.ArticleId == articleId);
        });
    }
}