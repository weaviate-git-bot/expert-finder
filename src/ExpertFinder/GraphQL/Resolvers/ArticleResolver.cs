using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL.Resolvers;

public class ArticleResolver
{
    public IQueryable<Article> GetArticles([Service] ApplicationDbContext dbContext)
    {
        return dbContext.Articles.OrderBy(x => x.Title);
    }
}