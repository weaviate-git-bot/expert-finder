namespace ExpertFinder.GraphQL.Models;

public class PublishArticleInput
{
    public Guid AuthorId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
