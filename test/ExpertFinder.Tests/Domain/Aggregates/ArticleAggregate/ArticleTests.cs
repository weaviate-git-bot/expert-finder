using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;
using ExpertFinder.Domain.Services;
using FakeItEasy;
using FluentAssertions;

namespace ExpertFinder.Tests.Domain.Aggregates.ArticleAggregate;

public class ArticleTests
{
    [Fact]
    public async Task Like_GeneratesDomainEvent()
    {
        var publishArticleCommand = new PublishArticleCommand(
            Guid.NewGuid(), "Test article", "Test content", Guid.NewGuid());
        
        var article = await Article.Publish(publishArticleCommand, A.Fake<IEmbeddingGenerator>());
        
        article.Like(new LikeArticleCommand(
            publishArticleCommand.ArticleId, Guid.NewGuid()));

        article.PendingDomainEvents
            .Where(x => x is ArticleLikedEvent).Should().NotBeEmpty();
    }

    [Fact]
    public async Task Publish_ReturnsAnArticle()
    {
        var publishArticleCommand = new PublishArticleCommand(
            Guid.NewGuid(), "Test article", "Test content", Guid.NewGuid());
        
        var article = await Article.Publish(publishArticleCommand, A.Fake<IEmbeddingGenerator>());

        article.Should().NotBeNull();
        article.PendingDomainEvents.Where(x => x is ArticlePublishedEvent).Should().NotBeEmpty();
    }
}