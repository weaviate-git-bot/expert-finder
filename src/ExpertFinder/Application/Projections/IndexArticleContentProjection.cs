using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;
using ExpertFinder.Domain.Services;
using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Shared;

namespace ExpertFinder.Application.Projections;

/// <summary>
/// This projection takes the article content and stores it in the vector database.
/// </summary>
public class IndexArticleContentProjection: IDomainEventHandler<ArticlePublishedEvent>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ISearchEngine _searchEngine;

    public IndexArticleContentProjection(ISearchEngine searchEngine, IArticleRepository articleRepository)
    {
        _searchEngine = searchEngine;
        _articleRepository = articleRepository;
    }

    public async Task Handle(ArticlePublishedEvent notification, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(notification.ArticleId);

        if (article == null)
        {
            return;
        }
        
        await _searchEngine.IndexArticleAsync(article);
    }
}