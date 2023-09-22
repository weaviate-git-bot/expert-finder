namespace ExpertFinder.Models;

public class Like
{
    public Guid UserId { get; set; }
    public Guid ArticleId { get; set; }
    public User User { get; set; } = default!;
    public Article Article { get; set; } = default!;
}