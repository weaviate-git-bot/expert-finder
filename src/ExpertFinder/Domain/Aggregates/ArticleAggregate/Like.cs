using ExpertFinder.Domain.Aggregates.UserAggregate;

namespace ExpertFinder.Domain.Aggregates.ArticleAggregate;

public class Like
{
    public Guid UserId { get; set; }
    public Guid ArticleId { get; set; }
}