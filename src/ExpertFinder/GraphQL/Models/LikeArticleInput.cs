namespace ExpertFinder.GraphQL.Models;

public class LikeArticleInput
{
    public Guid ArticleId { get; set; }
    public Guid UserId { get; set; }
}