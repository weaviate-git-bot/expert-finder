using ExpertFinder.Infrastructure.Persistence;
using ExpertFinder.Infrastructure.Services;

namespace ExpertFinder.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddPersistence(configuration);
        services.AddEmbeddingGenerator(configuration);
        services.AddContentManager();
        services.AddUnitOfWork();
        services.AddSearchEngine();
    }
}