using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Infrastructure.Persistence;

namespace ExpertFinder.GraphQL.Articles;

[ExtendObjectType(OperationTypeNames.Query)]
public class ArticleQueries
{
    [UsePaging]
    public IQueryable<Article> GetArticles([Service] ApplicationDbContext dbContext)
    {
        return dbContext.Articles.OrderBy(x => x.Title);
    }
}