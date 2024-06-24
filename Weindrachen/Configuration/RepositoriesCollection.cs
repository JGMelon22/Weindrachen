using Weindrachen.Infrastructure.Repositories;
using Weindrachen.Interfaces;

namespace Weindrachen.Configuration;

public static class RepositoriesCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IGrapeRepository, GrapeRepository>();
        services.AddScoped<IWineRepository, WineRepository>();
        services.AddScoped<IBrandGrapeWineRepository, BrandGrapeWineRepository>();

        return services;
    }
}