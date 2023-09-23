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
    }
}