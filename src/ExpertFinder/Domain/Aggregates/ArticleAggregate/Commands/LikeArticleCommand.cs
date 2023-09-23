using ExpertFinder.Shared;

namespace ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;

public record LikeArticleCommand(Guid ArticleId, Guid UserId) : ICommand;
