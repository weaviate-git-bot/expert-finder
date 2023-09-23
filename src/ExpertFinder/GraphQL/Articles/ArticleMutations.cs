using ExpertFinder.Domain.Aggregates.ArticleAggregate.Commands;
using MediatR;

namespace ExpertFinder.GraphQL.Articles;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class ArticleMutations
{
    public async Task<PublishArticlePayload> PublishArticleAsync(PublishArticleInput input, [Service] IMediator mediator)
    {
        var articleId = Guid.NewGuid();
        var publishArticleCommand = new PublishArticleCommand(articleId, input.Title, input.Body, input.AuthorId);
        
        await mediator.Send(publishArticleCommand);
        
        return new PublishArticlePayload
        {
            ArticleId = articleId,
        };
    }

    public async Task LikeArticleAsync(LikeArticleInput input, IMediator mediator)
    {
        await mediator.Send(new LikeArticleCommand(input.ArticleId, input.UserId));
    }
}