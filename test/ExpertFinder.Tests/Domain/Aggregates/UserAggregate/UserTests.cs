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
        var user = new User(Guid.NewGuid());
        var contentManager = A.Fake<IArticleRepository>();
        var embeddingCalculator = new EmbeddingCalculator();

        A.CallTo(() => contentManager.GetArticlesByAuthorId(A<Guid>.Ignored)).Returns(new List<Article>());

        await user.UpdateExpertise(contentManager, embeddingCalculator);

        user.ExpertiseEmbedding.Should().BeEquivalentTo(new float[1536]);
    }

    [Fact]
    public async Task UpdateExpertise_GivenOneArticle_ShouldUpdateExpertiseEmbedding()
    {
        var user = new User(Guid.NewGuid());
        var contentManager = A.Fake<IArticleRepository>();
        var embeddingCalculator = new EmbeddingCalculator();

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

        await user.UpdateExpertise(contentManager, embeddingCalculator);

        user.ExpertiseEmbedding.Should().BeEquivalentTo(embeddingCalculator.CombineEmbeddings(articles.Select(x => x.Embedding).ToList()));
    }

    [Fact]
    public async Task UpdateExpertise_GivenMultipleArticles_ShouldUpdateExpertiseEmbedding()
    {
        var user = new User(Guid.NewGuid());
        var contentManager = A.Fake<IArticleRepository>();
        var embeddingCalculator = new EmbeddingCalculator();

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

        await user.UpdateExpertise(contentManager, embeddingCalculator);

        user.ExpertiseEmbedding.Should().BeEquivalentTo(embeddingCalculator.CombineEmbeddings(articles.Select(x => x.Embedding).ToList()));
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
}