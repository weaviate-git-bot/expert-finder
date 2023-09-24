using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate.Events;
using ExpertFinder.Domain.Services;
using ExpertFinder.Shared;
using System.Numerics;

namespace ExpertFinder.Domain.Aggregates.UserAggregate;

public class User : AggregateRoot
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

    public async Task UpdateExpertise(IArticleRepository articleRepository, IEmbeddingCalculator embeddingCalculator)
    {
        var articles = await articleRepository.GetArticlesByAuthorId(this.Id);
        var embedding = embeddingCalculator.CombineEmbeddings(articles.Select(x => x.Embedding).ToList());

        EmitDomainEvent(new ExpertiseUpdatedEvent(this.Id, embedding));
    }

    protected override bool TryApplyDomainEvent(IDomainEvent evt)
    {
        switch (evt)
        {
            case ExpertiseUpdatedEvent expertiseUpdated:
                Apply(expertiseUpdated);
                break;
        }

        return true;
    }

    private void Apply(ExpertiseUpdatedEvent expertiseUpdated)
    {
        ExpertiseEmbedding = expertiseUpdated.ExpertiseEmbedding;
    }
}