using Crochet.Application.Database;
using Crochet.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Crochet.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IPostRepository, PostRepository>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, 
                string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(_ => 
                    new NpgsqlConnectionFactory(connectionString));

            services.AddSingleton<DbInitializer>();
            return services;
        }
    }
}
