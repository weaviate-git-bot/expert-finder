using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Application.CommandHandlers;

public class LikeArticleCommandHandler: ICommandHandler<LikeArticleCommand>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IPublisher _publisher;

    public LikeArticleCommandHandler(ApplicationDbContext applicationDbContext, IPublisher publisher)
    {
        _applicationDbContext = applicationDbContext;
        _publisher = publisher;
    }

    public async Task Handle(LikeArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _applicationDbContext.Articles.SingleAsync(x => x.Id == request.ArticleId);
        var user = await _applicationDbContext.Users.SingleAsync(x => x.Id == request.UserId);

        article.Like(request);
        
        await _applicationDbContext.SaveChangesAsync();

        foreach (var domainEvent in article.PendingDomainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}