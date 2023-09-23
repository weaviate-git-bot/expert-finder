using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Domain.Services;
using FakeItEasy;
using FluentAssertions;
using System.Numerics;

namespace ExpertFinder.Tests.Domain.Aggregates.UserAggregate;

public class UserTests
{
    [Fact]
    public async Task UpdateExpertise_GivenNoArticles_ShouldNotUpdateExpertiseEmbedding()
    {
        // Arrange
        var user = new User(Guid.NewGuid());
        var contentManager = A.Fake<IContentManager>();

        A.CallTo(() => contentManager.GetArticlesByAuthorId(A<Guid>.Ignored)).Returns(new List<Article>());

        // Act
        await user.UpdateExpertise(contentManager);

        // Assert
        user.ExpertiseEmbedding.Should().BeEquivalentTo(new float[1536]);
    }

    [Fact]
    public async Task UpdateExpertise_GivenOneArticle_ShouldUpdateExpertiseEmbedding()
    {
        // Arrange
        var user = new User(Guid.NewGuid());
        var contentManager = A.Fake<IContentManager>();

        var articles = new List<Article>
        {
            new Article(Guid.NewGuid())
            {
                Title = "Test content",
                Body = "Test article",
                Embedding = GenerateRandomEmbedding()
            }
        };
        
        A.CallTo(() => contentManager.GetArticlesByAuthorId(A<Guid>.Ignored)).Returns(articles);

        // Act
        await user.UpdateExpertise(contentManager);

        // Assert
        user.ExpertiseEmbedding.Should().BeEquivalentTo(CalculateDotProduct(articles));
    }

    [Fact]
    public async Task UpdateExpertise_GivenMultipleArticles_ShouldUpdateExpertiseEmbedding()
    {
        // Arrange
        var user = new User(Guid.NewGuid());
        var contentManager = A.Fake<IContentManager>();

        var articles = new List<Article>
        {
            new Article(Guid.NewGuid())
            {
                Title = "Test content",
                Body = "Test article",
                Embedding = GenerateRandomEmbedding()
            },
            new Article(Guid.NewGuid())
            {
                Title = "Test content",
                Body = "Test article",
                Embedding = GenerateRandomEmbedding()
            },
            new Article(Guid.NewGuid())
            {
                Title = "Test content",
                Body = "Test article",
                Embedding = GenerateRandomEmbedding()
            }
        };
        
        A.CallTo(() => contentManager.GetArticlesByAuthorId(A<Guid>.Ignored)).Returns(articles);

        // Act
        await user.UpdateExpertise(contentManager);

        // Assert
        user.ExpertiseEmbedding.Should().BeEquivalentTo(CalculateDotProduct(articles));
    }
    
    private float[] GenerateRandomEmbedding()
    {
        var random = new Random((int)DateTime.Now.Ticks);
        var embedding = new float[1536];
        
        for (var i = 0; i < 1536; i++)
        {
            embedding[i] = (float)random.NextDouble();
        }

        return embedding;
    }

    private float[] CalculateDotProduct(List<Article> articles)
    {
        Vector<float> total = new Vector<float>(new float[1536]);

        // Calculate the new expertise embedding based on the articles that the user wrote.
        // We're multiplying the vectors here so that we get a shared vector representing all the articles
        // that the user wrote.
        for (var index = 0; index < articles.Count; index++)
        {
            if (index == 0)
            {
                total = new Vector<float>(articles[index].Embedding.ToArray());
            }
            else
            {
                var embeddingVector = new Vector<float>(articles[index].Embedding.ToArray());
                total = Vector.Multiply(total, embeddingVector);
            }
        }
        
        var result = new float[1536];
        total.CopyTo(result);

        return result;
    }
}