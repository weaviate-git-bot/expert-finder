using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL;

public class Query
{
    public Article? GetArticle(Guid id, ApplicationDbContext dbContext) => dbContext.Articles.FirstOrDefault(x => x.Id == id);
}