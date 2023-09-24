using ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;
using ExpertFinder.Domain.Services;
using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Shared;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Application.Projections;

/// <summary>
/// This projection updates the expertise profile of the author after an article is published.
/// </summary>
public class UpdateExpertiseProfileProjection: IDomainEventHandler<ArticlePublishedEvent>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IArticleRepository _articleRepository;
    private readonly ISearchEngine _searchEngine;

    public UpdateExpertiseProfileProjection(ApplicationDbContext applicationDbContext, IArticleRepository articleRepository, ISearchEngine searchEngine)
    {
        _applicationDbContext = applicationDbContext;
        _articleRepository = articleRepository;
        _searchEngine = searchEngine;
    }

    public async Task Handle(ArticlePublishedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.SingleAsync(
            x => x.Id == notification.AuthorId, 
            cancellationToken: cancellationToken);
        
        await user.UpdateExpertise(_articleRepository);

        await _searchEngine.IndexExpertProfileAsync(user);
    }
}