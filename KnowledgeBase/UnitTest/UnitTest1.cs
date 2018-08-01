using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SSOManagerLib.Dapper;
using SSOManagerLib.Model;
using SSOManagerLib.UserService;
using System;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var builder = new ContainerBuilder();
            IServiceCollection services = new ServiceCollection();
            

            //services.Configure<MysqlOption>();
            services.AddSingleton<DapperContext>();
            builder.RegisterType(typeof(DapperRepository)).As(typeof(IDapperRepository)).SingleInstance();
            builder.RegisterType(typeof(UserServices)).As(typeof(IUserServices)).SingleInstance();
            builder.Populate(services);
            var container = builder.Build();
            var test= container.Resolve<IUserServices>();
            var f = test.GetDepartList();
            Console.WriteLine(f.Count);

        }
    }
}
