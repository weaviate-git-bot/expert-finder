using ExpertFinder.Application.Projections;
using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Domain.Services;
using ExpertFinder.Shared;
using FakeItEasy;

namespace ExpertFinder.Tests.Application.Projections;

public class UpdateExpertiseProfileProjectionTests
{
    [Fact]
    public async Task Handle_ShouldUpdateUserProfile()
    {
        var userId = Guid.NewGuid();

        var userRepository = A.Fake<IUserRepository>();
        var articleRepository = A.Fake<IArticleRepository>();
        var searchEngine = A.Fake<ISearchEngine>();
        var unitOfWork = A.Fake<IUnitOfWork>();
        var embeddingCalculator = new EmbeddingCalculator();

        A.CallTo(() => userRepository.GetUserByIdAsync(A<Guid>._)).Returns(new User(userId));

        var projection = new UpdateExpertiseProfileProjection(userRepository, articleRepository, searchEngine, unitOfWork, embeddingCalculator);

        var articlePublishedEvent = new ArticlePublishedEvent(
            Guid.NewGuid(), "Test", "Test",
            userId, new float[1536]);

        await projection.Handle(articlePublishedEvent, CancellationToken.None);

        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._)).MustHaveHappened();
        A.CallTo(() => searchEngine.IndexExpertProfileAsync(A<User>._)).MustHaveHappened();
    }
}