using System.Numerics;

namespace ExpertFinder.Domain.Services;

public class EmbeddingCalculator : IEmbeddingCalculator
{
    public float[] CombineEmbeddings(List<float[]> embeddings)
    {
        Vector<float> combinedVector = Vector<float>.Zero;

        for (var i = 0; i < embeddings.Count; i++)
        {
            if (i == 0)
            {
                combinedVector = new Vector<float>(embeddings[i]);
            }
            else
            {
                var localVector = new Vector<float>(embeddings[i]);
                combinedVector = Vector.Multiply(combinedVector, localVector);
            }
        }

        var output = new float[1536];
        combinedVector.CopyTo(output);

        return output;
    }
}