using ExpertFinder.Shared;

namespace ExpertFinder.Domain.Aggregates.UserAggregate.Events;

public record ExpertiseUpdatedEvent(Guid UserId, float[] ExpertiseEmbedding) : IDomainEvent;