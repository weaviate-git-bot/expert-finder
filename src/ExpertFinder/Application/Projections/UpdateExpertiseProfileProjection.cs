using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;
using ExpertFinder.Domain.Aggregates.UserAggregate;
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
    private readonly IArticleRepository _articleRepository;
    private readonly ISearchEngine _searchEngine;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UpdateExpertiseProfileProjection(IUserRepository userRepository, IArticleRepository articleRepository, ISearchEngine searchEngine, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _searchEngine = searchEngine;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ArticlePublishedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(notification.AuthorId);

        if (user == null)
        {
            return;
        }
        
        await user.UpdateExpertise(_articleRepository);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _searchEngine.IndexExpertProfileAsync(user);
    }
}