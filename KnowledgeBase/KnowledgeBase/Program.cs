using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace KnowledgeBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://*:8000")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}