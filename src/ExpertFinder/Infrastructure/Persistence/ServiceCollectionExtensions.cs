using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddPersistence(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultDatabase"));
        });


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
    }
}