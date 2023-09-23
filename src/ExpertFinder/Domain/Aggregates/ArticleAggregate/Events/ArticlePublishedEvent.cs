using ExpertFinder.Shared;

namespace ExpertFinder.Domain.Aggregates.ArticleAggregate.Events;

public record ArticlePublishedEvent(Guid ArticleId, string Title, string Body, Guid AuthorId, float[] Embedding): IDomainEvent;
