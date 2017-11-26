using HeadHunterTest.Database;
using HeadHunterTest.Web.Extension;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HeadHunterTest.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDatabase<DatabaseContext>()
                .SetUpWithService<DatabaseInitializer>(x=>x.Initialize().Wait())
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
