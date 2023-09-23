using Azure;
using Azure.AI.OpenAI;
using ExpertFinder.Domain.Services;
using Microsoft.Extensions.Options;

namespace ExpertFinder.Infrastructure.Services;

public static class ServiceCollectionExtensions
{
    public static void AddEmbeddingGenerator(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddScoped<OpenAIClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<EmbeddingGeneratorOptions>>();
            return new OpenAIClient(new Uri(options.Value.Endpoint), new AzureKeyCredential(options.Value.ApiKey));
        });

        services.AddScoped<IEmbeddingGenerator, EmbeddingGenerator>();
        
        services.Configure<EmbeddingGeneratorOptions>(configuration.GetSection("EmbeddingGenerator"));
    }

    public static void AddContentManager(this IServiceCollection services)
    {
        services.AddScoped<IContentManager, ContentManager>();
    }
}