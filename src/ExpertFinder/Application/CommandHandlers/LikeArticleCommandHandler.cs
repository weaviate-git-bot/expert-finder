using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Application.CommandHandlers;

public class LikeArticleCommandHandler: ICommandHandler<LikeArticleCommand>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public LikeArticleCommandHandler(IArticleRepository articleRepository, IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task Handle(LikeArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(request.ArticleId);

        if (article == null)
        {
            return;
        }
        
        article.Like(request);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in article.PendingDomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
    }
}