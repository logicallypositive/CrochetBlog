using Crochet.Application.Database;
using Crochet.Application.Repositories;
using Crochet.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Crochet.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IPostRepository, PostRepository>();
        services.AddSingleton<IPostService, PostService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddSingleton<IDbConnectionFactory>(
            _ => new NpgsqlConnectionFactory(connectionString)
        );

        services.AddSingleton<DbInitializer>();

        return services;
    }
}
