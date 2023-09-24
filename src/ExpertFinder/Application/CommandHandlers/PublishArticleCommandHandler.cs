using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Domain.Services;
using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Shared;
using MediatR;

namespace ExpertFinder.Application.CommandHandlers;

public class PublishArticleCommandHandler : ICommandHandler<PublishArticleCommand>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IPublisher _publisher;
    private readonly IEmbeddingGenerator _embeddingGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public PublishArticleCommandHandler(IArticleRepository articleRepository, IPublisher publisher, IEmbeddingGenerator embeddingGenerator, IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _publisher = publisher;
        _embeddingGenerator = embeddingGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await Article.Publish(request, _embeddingGenerator);

        await _articleRepository.CreateAsync(article);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var pendingDomainEvent in article.PendingDomainEvents)
        {
            await _publisher.Publish(pendingDomainEvent, cancellationToken);
        }
    }
}