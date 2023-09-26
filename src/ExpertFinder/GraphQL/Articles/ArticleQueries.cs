using ExpertFinder.Domain.Aggregates.ArticleAggregate;

namespace ExpertFinder.GraphQL.Articles;

[ExtendObjectType(OperationTypeNames.Query)]
public class ArticleQueries
{
    public async Task<Article?> GetArticleAsync(Guid id, [Service] IArticleRepository articleRepository)
    {
        return await articleRepository.GetArticleByIdAsync(id);
    }

    [UsePaging]
    public IQueryable<Article> GetArticles([Service] IArticleRepository articleRepository)
    {
        return articleRepository.GetArticles();
    }
}