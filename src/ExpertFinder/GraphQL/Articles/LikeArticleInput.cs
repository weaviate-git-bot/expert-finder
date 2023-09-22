namespace ExpertFinder.GraphQL.Articles;

public class LikeArticleInput
{
    public Guid ArticleId { get; set; }
    public Guid UserId { get; set; }
}