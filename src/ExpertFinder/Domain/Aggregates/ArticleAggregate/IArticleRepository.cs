namespace ExpertFinder.Domain.Aggregates.ArticleAggregate;

public interface IArticleRepository
{
    Task CreateAsync(Article article);
    Task<Article?> GetArticleByIdAsync(Guid articleId);
    Task<IReadOnlyList<Article>> GetArticlesByAuthorId(Guid authorId);
}