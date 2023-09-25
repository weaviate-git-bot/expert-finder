using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Domain.Services;
using ExpertFinder.Weaviate;

namespace ExpertFinder.Infrastructure.Services;

public class SearchEngine : ISearchEngine
{
    private IVectorDatabaseClient _client;

    public SearchEngine(IVectorDatabaseClient client)
    {
        _client = client;
    }

    public async Task IndexArticleAsync(Article article)
    {
        if (!await _client.ObjectExistsAsync("Article", article.Id))
        {
            var entry = new VectorObject
            {
                Id = article.Id,
                Class = "Article",
                Properties = {
                ["Title"] = article.Title,
                ["Body"] = article.Body,
                ["AuthorId"] = article.AuthorId
            },
                Vector = article.Embedding
            };

            await _client.CreateObjectAsync(entry);
        }
        else
        {
            var entry = await _client.GetObjectAsync("Article", article.Id);
            entry!.Vector = article.Embedding;

            await _client.UpdateObjectAsync(entry);
        }

    }

    public async Task IndexExpertProfileAsync(User user)
    {
        if (!await _client.ObjectExistsAsync("Expert", user.Id))
        {
            var entry = new VectorObject
            {
                Id = user.Id,
                Class = "Expert",
                Properties = {
                    ["FullName"] = user.FullName,
                },
                Vector = user.ExpertiseEmbedding
            };

            await _client.CreateObjectAsync(entry);
        }
        else
        {
            var entry = await _client.GetObjectAsync("Expert", user.Id);
            entry!.Vector = user.ExpertiseEmbedding;

            await _client.UpdateObjectAsync(entry);
        }
    }
}