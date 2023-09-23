namespace ExpertFinder.Domain.Services;

public interface IEmbeddingGenerator
{
    Task<float[]> GenerateEmbeddingAsync(string text);
}