using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSOManagerLib.Dapper;
using SSOManagerLib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSOManagerLib
{
   public static class UseSSODb
    {
        public static IServiceCollection UseSSOmysql(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.Configure<MysqlOption>(configurationSection);
            services.AddSingleton<DapperContext>();
            return services;
        }

    }
}
