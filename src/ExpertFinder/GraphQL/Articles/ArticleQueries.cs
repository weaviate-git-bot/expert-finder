using ExpertFinder.Domain.Aggregates.ArticleAggregate;

namespace ExpertFinder.GraphQL.Articles;

[ExtendObjectType(OperationTypeNames.Query)]
public class ArticleQueries
{
    [UsePaging]
    public IQueryable<Article> GetArticles([Service] IArticleRepository articleRepository)
    {
        return articleRepository.GetArticles();
    }
}