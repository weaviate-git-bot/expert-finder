using ExpertFinder.Domain.Aggregates.ArticleAggregate;

namespace ExpertFinder.Domain.Services;

public interface IContentManager
{
    Task<Article?> GetArticleByIdAsync(Guid articleId);
    Task<IReadOnlyList<Article>> GetArticlesByAuthorId(Guid authorId);
}