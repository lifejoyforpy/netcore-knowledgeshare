using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Application.Service;
using BoeSj.KnowledgeBase.Infrastructure.Mongo;
using BoeSj.KnowledgeBase.Infrastructure.Search;
using BoeSj.KnowledgeBase.Repository.Interface;
using BoeSj.KnowledgeBase.Repository.Repository;
using BoeSj.KnowledgeBase.Repository.UnitOfWork;
using BoeSj.KnowledgeBase.Web.Filter;
using Core.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSOManagerLib;
using SSOManagerLib.Dapper;
using SSOManagerLib.Model;
using SSOManagerLib.UserService;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace KnowledgeBase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddMvc(opt => opt.Filters.Add<KnowledgeExceptionFilter>()).AddJsonOptions(options => options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");
            services.AddDbContext<KnowledgeContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySqlConnection")));
            services.AddScoped<DbContext>(provider => provider.GetService<KnowledgeContext>());
            services.UserMongoLog(Configuration.GetSection("Mongodb"));
            services.UserConfg<filePath>(Configuration.GetSection("filePath"));
            //注册redis服务
            services.Configure<CacheOption>(Configuration.GetSection("Redis"));
            services.AddSingleton<ICache, RedisCache>();
            services.AddSingleton<SSOHelper>();
            //注册特性扫描服务
            services.Configure<ModuleOption>(Configuration.GetSection("DllFilter"));
            services.AddSingleton<AddAttibute>();
            //注入排队的后台任务
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            //注入elasticsearch 服务
            services.UserElasticSearch(Configuration.GetSection("ElasticSearch"));
            //注入IHttpContextAccessor,实现httpconext.current
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper();

          

            //session
            services.AddDistributedMemoryCache();
            services.AddSession(o => { o.IdleTimeout = TimeSpan.FromHours(24);
                o.Cookie.HttpOnly = true;

            }
            );
            //注入SSO用户信息相关信息
            services.UseSSOmysql(Configuration.GetSection("SSODb"));
            services.AddScoped<IDapperRepository, DapperRepository>();
            services.AddScoped<IUserServices, UserServices>();
            // services.AddAuthentication();
            //core
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AnyOrigin", builder =>
            //    {
            //        builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod();
            //    });
            //});
            //services.AddMemoryCache();
            services.AddSingleton<IMemoryCache>(fa =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddSingleton<ICacheRepository, MemoryCacheRepository>();

            var containerBuilder = new ContainerBuilder();
            var repositoryassembly = Assembly.Load("BoeSj.KnowledgeBase.Repository");
            var applicationassembly = Assembly.Load("BoeSj.KnowledgeBase.Application");
            var asses = new Assembly[] { repositoryassembly, applicationassembly };
            containerBuilder.RegisterAssemblyTypes(asses)
             .AsImplementedInterfaces().InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //containerBuilder.RegisterGeneric(typeof(SearchRepository<>))
            //.As(typeof(ISearchRepository<>)).SingleInstance();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        {
            //配置服务
            //SSOManager.HttpContext.ServiceProvider = svp;
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //session
            app.UseSession();

            app.UseStaticFiles();
            // Configure
            //app.UseCors("AnyOrigin");
            //  app.UseAuthentication();
            //创建routehandler。创建路由规则maproute，创建routebuilder对象，
            var routehandler = new RouteHandler(async context =>
            {
                var c = context.GetRouteValue("controller");
                var a = context.GetRouteValue("action");
                Console.WriteLine(a.ToString(), c.ToString());
            });
            //注册中间件=>该中间件目的是吧路由信息存在httpcontext里，让权限认证中间件获取
            var routeBuilder = new RouteBuilder(app)
            {
                DefaultHandler = routehandler,
            };
            routeBuilder.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            var routes = routeBuilder.Build();
            app.Use(async (context, next) =>
            {  
                
                RouteContext routeContext = new RouteContext(context);
                routeContext.RouteData.Routers.Add(routes);
                //初始化吧routehander挂在到routeContext
                await routes.RouteAsync(routeContext);              
                context.Features.Set<IRoutingFeature>(new RoutingFeature { RouteData = routeContext.RouteData });
                await next();
            });
            app.UseAuthorize(
                Configuration["SSO:noauth_url"].ToString(), Configuration["SSO:login_url"].ToString()
             );
           
            app.UseMvc(route =>
            {
                route.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                route.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class RouterGetMiddleware
    {
        private RequestDelegate _next;
        public RouterGetMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var a = context.GetRouteData();
            var b = context.GetRouteValue("controller");
            var routeValues = context.GetRouteData().Values;
            await _next(context);
        }
    }
    public static class RouterGetBuilderExtensions
    {
        public static IApplicationBuilder UseRouterGet(this IApplicationBuilder builder,  Action<IRouteBuilder> action)
        {

            RouteBuilder routeBuilder = new RouteBuilder(builder);
            action(routeBuilder);
            return builder.UseRouter(routeBuilder.Build());
        }
    }



}