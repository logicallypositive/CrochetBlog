using Crochet.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Crochet.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            return services;
        }
    }
}
