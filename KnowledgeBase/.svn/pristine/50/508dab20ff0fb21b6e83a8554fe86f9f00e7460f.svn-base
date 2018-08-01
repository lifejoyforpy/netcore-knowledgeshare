using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoeSj.KnowledgeBase.Infrastructure.Search
{
    public static class ElasticSearchExtensions
    {
        public static IServiceCollection UserElasticSearch(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.Configure<ElasticSearchOptions>(configurationSection);
            services.AddSingleton<ElasticSearchContext>();
            return services;
        }
    }
}
