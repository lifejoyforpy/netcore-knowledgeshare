using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoeSj.KnowledgeBase.Infrastructure.Mongo
{
    public static class ConfgExtensions
    {
        public static IServiceCollection UserConfg<TEntity>(this IServiceCollection services, IConfigurationSection configurationSection) where TEntity : class, new()
        {
            services.Configure<TEntity>(configurationSection);
            services.AddSingleton<ConfgContext<TEntity>>();
            return services;
        }
    }
}