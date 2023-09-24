namespace ExpertFinder.Domain.Services;

public interface IEmbeddingCalculator
{
    float[] CombineEmbeddings(List<float[]> embeddings);
}