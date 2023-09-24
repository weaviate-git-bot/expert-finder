using ExpertFinder.Application.CommandHandlers;
using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Domain.Services;
using ExpertFinder.Shared;
using FakeItEasy;
using MediatR;

namespace ExpertFinder.Tests.Application.CommandHandlers;

public class PublishArticleCommandHandlerTests
{
    [Fact]
    public async Task Handle_CreatesArticle()
    {
        var articleId = Guid.NewGuid();
        var authorId = Guid.NewGuid();

        var articleRepository = A.Fake<IArticleRepository>();
        var publisher = A.Fake<IPublisher>();
        var embeddingGenerator = A.Fake<IEmbeddingGenerator>();
        var unitOfWork = A.Fake<IUnitOfWork>();

        var handler = new PublishArticleCommandHandler(articleRepository, publisher, embeddingGenerator, unitOfWork);

        await handler.Handle(new PublishArticleCommand(articleId, "Test", "Test", authorId), CancellationToken.None);

        A.CallTo(() => publisher.Publish(A<IDomainEvent>._, A<CancellationToken>._)).MustHaveHappened();
        A.CallTo(() => embeddingGenerator.GenerateEmbeddingAsync(A<string>._)).MustHaveHappened();
        A.CallTo(() => articleRepository.CreateAsync(A<Article>._)).MustHaveHappened();
    }
}