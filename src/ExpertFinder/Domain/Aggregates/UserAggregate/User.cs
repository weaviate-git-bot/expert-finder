using ExpertFinder.Domain.Services;
using System.Numerics;

namespace ExpertFinder.Domain.Aggregates.UserAggregate;

public class User
{
    private User()
    {
        
    }

    public User(Guid userId)
    {
        Id = userId;
    }
    
    public Guid Id { get; set; }
    public string FullName { get; set; } = "";
    public float[] ExpertiseEmbedding { get; set; } = new float[1536];

    public async Task UpdateExpertise(IContentManager contentManager)
    {
        var articles = await contentManager.GetArticlesByAuthorId(this.Id);
        
        Vector<float> expertiseEmbedding = new Vector<float>(new float[1536]);

        // Calculate the new expertise embedding based on the articles that the user wrote.
        // We're multiplying the vectors here so that we get a shared vector representing all the articles
        // that the user wrote.
        for (var index = 0; index < articles.Count; index++)
        {
            if (index == 0)
            {
                expertiseEmbedding = new Vector<float>(articles[index].Embedding.ToArray());
            }
            else
            {
                var embeddingVector = new Vector<float>(articles[index].Embedding.ToArray());
                expertiseEmbedding = Vector.Multiply(expertiseEmbedding, embeddingVector);
            }
        }
        
        expertiseEmbedding.CopyTo(ExpertiseEmbedding);
    }
}