using Microsoft.Extensions.DependencyInjection;

namespace ExpertFinder.Weaviate;

public static class ServiceCollectionExtensions
{
    public static void AddWeaviateClient(this IServiceCollection services, Uri endpointUri)
    {
        services.AddHttpClient<IVectorDatabaseClient, VectorDatabaseClient>(client =>
        {
            client.BaseAddress = endpointUri;
        });
    }
}