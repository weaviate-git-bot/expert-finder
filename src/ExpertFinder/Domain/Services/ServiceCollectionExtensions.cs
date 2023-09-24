namespace ExpertFinder.Domain.Services;

public static class ServiceCollectionExtensions
{
    public static void AddEmbeddingCalculator(this IServiceCollection services)
    {
        services.AddSingleton<IEmbeddingCalculator, EmbeddingCalculator>();
    }
}