using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using ExpertFinder.Domain.Services;
using ExpertFinder.Weaviate;

namespace ExpertFinder.Infrastructure.Services;

public class SearchEngine : ISearchEngine
{
    private IVectorDatabaseClient _client;
    private readonly WeaviateSearch _searchClient;

    public SearchEngine(IVectorDatabaseClient client, WeaviateSearch searchClient)
    {
        _client = client;
        _searchClient = searchClient;
    }

    public async Task<IEnumerable<ExpertSearchResult>> FindExpertAsync(Article article)
    {
        var result = await _searchClient.GetExperts.ExecuteAsync(article.Embedding.Select(x => (double)x).ToList(), 0.3f);
        return result.Data!.Get!.Expert!.Select(x => new ExpertSearchResult(Guid.Parse(x!._additional!.Id!), x!.FullName!)).ToList();
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