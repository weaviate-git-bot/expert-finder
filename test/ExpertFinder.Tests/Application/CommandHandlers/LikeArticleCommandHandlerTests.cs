using ExpertFinder.Application.CommandHandlers;
using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Shared;
using FakeItEasy;
using MediatR;

namespace ExpertFinder.Tests.Application.CommandHandlers;

public class LikeArticleCommandHandlerTests
{
    [Fact]
    public async Task Handle_UpdatesLikes()
    {
        var userId = Guid.NewGuid();
        var articleId = Guid.NewGuid();
        
        var articleRepository = A.Fake<IArticleRepository>();
        var unitOfWork = A.Fake<IUnitOfWork>();
        var publisher = A.Fake<IPublisher>();

        A.CallTo(() => articleRepository.GetArticleByIdAsync(A<Guid>._))
            .Returns(new Article(articleId));
        
        var handler = new LikeArticleCommandHandler(articleRepository, unitOfWork, publisher);

        await handler.Handle(new LikeArticleCommand(articleId, userId), CancellationToken.None);

        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._)).MustHaveHappened();
        A.CallTo(() => publisher.Publish(A<IDomainEvent>._, A<CancellationToken>._)).MustHaveHappened();
    }
}