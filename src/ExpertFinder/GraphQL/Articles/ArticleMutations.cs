using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL.Articles;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class ArticleMutations
{
    public PublishArticlePayload PublishArticle(PublishArticleInput input, [Service] ApplicationDbContext dbContext)
    {
        var author = dbContext.Users.SingleOrDefault(x => x.Id == input.AuthorId);

        if (author == null)
        {
            throw new InvalidOperationException("Please select an existing author");
        }

        var article = new Article
        {
            Author = author,
            Body = input.Content,
            Title = input.Title,
            Id = Guid.NewGuid()
        };

        dbContext.Articles.Add(article);
        dbContext.SaveChanges();

        return new PublishArticlePayload
        {
            ArticleId = article.Id,
        };
        
    }

    public LikeArticlePayload LikeArticle(LikeArticleInput input, [Service] ApplicationDbContext dbContext)
    {
        var like = dbContext.Likes.SingleOrDefault(x => x.ArticleId == input.ArticleId && x.UserId == input.UserId);

        if (like == null)
        {
            like = new Like
            {
                UserId = input.UserId,
                ArticleId = input.ArticleId
            };

            dbContext.Likes.Add(like);
            dbContext.SaveChanges();

        }

        return new LikeArticlePayload
        {
            Likes = dbContext.Likes.Count(x => x.ArticleId == input.ArticleId)
        };
    }
}