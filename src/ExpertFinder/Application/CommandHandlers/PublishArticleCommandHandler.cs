using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Domain.Services;
using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Shared;
using MediatR;

namespace ExpertFinder.Application.CommandHandlers;

public class PublishArticleCommandHandler: ICommandHandler<PublishArticleCommand>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IPublisher _publisher;
    private readonly IEmbeddingGenerator _embeddingGenerator;

    public PublishArticleCommandHandler(ApplicationDbContext applicationDbContext, IPublisher publisher, IEmbeddingGenerator embeddingGenerator)
    {
        _applicationDbContext = applicationDbContext;
        _publisher = publisher;
        _embeddingGenerator = embeddingGenerator;
    }

    public async Task Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await Article.Publish(request, _embeddingGenerator);

        await _applicationDbContext.AddAsync(article, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        foreach (var pendingDomainEvent in article.PendingDomainEvents)
        {
            await _publisher.Publish(pendingDomainEvent, cancellationToken);
        }
    }
}