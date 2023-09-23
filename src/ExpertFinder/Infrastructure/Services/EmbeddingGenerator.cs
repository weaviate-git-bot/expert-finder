using Azure.AI.OpenAI;
using ExpertFinder.Domain.Services;

namespace ExpertFinder.Infrastructure.Services;

public class EmbeddingGenerator: IEmbeddingGenerator
{
    private readonly OpenAIClient _client;

    public EmbeddingGenerator(OpenAIClient client)
    {
        _client = client;
    }

    public async Task<float[]> GenerateEmbeddingAsync(string text)
    {
        var result = await _client.GetEmbeddingsAsync(
            "embedding", new EmbeddingsOptions(text));
        
        return result.Value.Data[0].Embedding.ToArray();
    }
}