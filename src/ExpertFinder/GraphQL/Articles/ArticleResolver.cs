using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Infrastructure.Persistence;

namespace ExpertFinder.GraphQL.Articles;

public class ArticleResolver
{
    public IQueryable<Article> GetArticles(ApplicationDbContext dbContext)
    {
        return dbContext.Articles.OrderBy(x => x.Title);
    }
}