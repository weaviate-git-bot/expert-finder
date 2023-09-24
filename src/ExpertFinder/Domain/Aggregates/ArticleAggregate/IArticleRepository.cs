namespace ExpertFinder.Domain.Aggregates.ArticleAggregate;

public interface IArticleRepository
{
    Task<Article?> GetArticleByIdAsync(Guid articleId);
    Task<IReadOnlyList<Article>> GetArticlesByAuthorId(Guid authorId);
}