using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate;

namespace ExpertFinder.Domain.Services;

public interface ISearchEngine
{
    Task IndexArticleAsync(Article article);
    Task IndexExpertProfileAsync(User user);

    Task<IEnumerable<ExpertSearchResult>> FindExpertAsync(Article article);
}