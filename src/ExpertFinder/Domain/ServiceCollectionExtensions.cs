using ExpertFinder.Domain.Services;

namespace ExpertFinder.Domain;

public static class ServiceCollectionExtensions
{
    public static void AddDomainLayer(this IServiceCollection services)
    {
        services.AddEmbeddingCalculator();
    }
}