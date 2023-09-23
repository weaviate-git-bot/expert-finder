using ExpertFinder.Shared;

namespace ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;

public record PublishArticleCommand(Guid ArticleId, string Title, string Body, Guid AuthorId) : ICommand;
