using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Domain.Services;

namespace ExpertFinder.Infrastructure.Services;

public class SearchEngine : ISearchEngine
{
    public Task IndexArticleAsync(Article article)
    {
        throw new NotImplementedException();
    }

    public Task IndexExpertProfileAsync(User user)
    {
        throw new NotImplementedException();
    }
}