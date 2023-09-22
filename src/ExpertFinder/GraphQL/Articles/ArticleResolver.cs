using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL.Articles;

public class ArticleResolver
{
    public IQueryable<Article> GetArticles(ApplicationDbContext dbContext)
    {
        return dbContext.Articles.OrderBy(x => x.Title);
    }
}