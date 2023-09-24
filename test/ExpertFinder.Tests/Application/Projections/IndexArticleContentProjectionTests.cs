using ExpertFinder.Application.Projections;
using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;
using ExpertFinder.Domain.Services;
using FakeItEasy;

namespace ExpertFinder.Tests.Application.Projections;

public class IndexArticleContentProjectionTests
{
    [Fact]
    public async Task Handle_ArticlePublishedEvent_IndexesArticle()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        
        var contentManager = A.Fake<IArticleRepository>();
        var searchEngine = A.Fake<ISearchEngine>();

        var publishArticleCommand = new PublishArticleCommand(articleId, "Test content", "Test content", authorId);
        var embeddingGenerator = A.Fake<IEmbeddingGenerator>();

        A.CallTo(() => embeddingGenerator.GenerateEmbeddingAsync(A<string>._)).Returns(new float[1536]);
        
        var article = await Article.Publish(publishArticleCommand, embeddingGenerator);
        
        A.CallTo(() => contentManager.GetArticleByIdAsync(articleId)).Returns(article);
        
        var projection = new IndexArticleContentProjection(searchEngine, contentManager);
        var notification = new ArticlePublishedEvent(articleId, "Test content", "Test content", authorId, new float[1536]);

        // Act
        await projection.Handle(notification, CancellationToken.None);

        // Assert
        A.CallTo(() => searchEngine.IndexArticleAsync(article)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_ArticleNotFound_DoesNotIndexArticle()
    {
        // Arrange
        var articleId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        
        var contentManager = A.Fake<IArticleRepository>();
        var searchEngine = A.Fake<ISearchEngine>();
        
        A.CallTo(() => contentManager.GetArticleByIdAsync(articleId)).Returns(null as Article);
        
        var projection = new IndexArticleContentProjection(searchEngine, contentManager);
        var notification = new ArticlePublishedEvent(articleId, "Test content", "Test content", authorId, new float[1536]);

        // Act
        await projection.Handle(notification, CancellationToken.None);

        // Assert
        A.CallTo(() => searchEngine.IndexArticleAsync(A<Article>._)).MustNotHaveHappened();
    }
}