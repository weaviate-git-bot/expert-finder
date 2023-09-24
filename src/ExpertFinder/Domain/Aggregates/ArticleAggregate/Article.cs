using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;
using ExpertFinder.Domain.Services;
using ExpertFinder.Shared;

namespace ExpertFinder.Domain.Aggregates.ArticleAggregate;

public class Article : AggregateRoot
{
    private Article()
    {
    }

    public Article(Guid articleId)
    {
        Id = articleId;
    }

    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    public string Body { get; set; } = "";

    public Guid AuthorId { get; set; }

    public float[] Embedding { get; set; } = new float[1536];

    public List<Like> Likes { get; set; } = new();

    public void Like(LikeArticleCommand cmd)
    {
        EmitDomainEvent(new ArticleLikedEvent(cmd.ArticleId, cmd.UserId));
    }

    public static async Task<Article> Publish(PublishArticleCommand cmd, IEmbeddingGenerator embeddingGenerator)
    {
        var article = new Article(cmd.ArticleId);
        var articleEmbedding = await embeddingGenerator.GenerateEmbeddingAsync(cmd.Body);

        article.EmitDomainEvent(new ArticlePublishedEvent(
            cmd.ArticleId, cmd.Title, cmd.Body, cmd.AuthorId, articleEmbedding));

        return article;
    }

    protected override bool TryApplyDomainEvent(IDomainEvent evt)
    {
        switch (evt)
        {
            case ArticlePublishedEvent articlePublishedEvent:
                Apply(articlePublishedEvent);
                break;
            case ArticleLikedEvent articleLikedEvent:
                Apply(articleLikedEvent);
                break;
            default:
                return false;
        }

        return true;
    }

    private void Apply(ArticlePublishedEvent articlePublishedEvent)
    {
        Title = articlePublishedEvent.Title;
        Body = articlePublishedEvent.Body;
        AuthorId = articlePublishedEvent.AuthorId;
        Embedding = articlePublishedEvent.Embedding;
    }

    private void Apply(ArticleLikedEvent articleLikedEvent)
    {
        Likes.Add(new Like { UserId = articleLikedEvent.UserId, ArticleId = articleLikedEvent.ArticleId });
    }
}