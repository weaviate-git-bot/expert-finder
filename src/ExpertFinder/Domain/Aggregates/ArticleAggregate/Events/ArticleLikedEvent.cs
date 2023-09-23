using ExpertFinder.Shared;

namespace ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;

public record ArticleLikedEvent(Guid ArticleId, Guid UserId): IDomainEvent;