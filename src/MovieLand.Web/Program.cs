using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieLand.Infrastructure.Data;
using System;


namespace MovieLand.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            SeedDatabase(host);

            host.Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        private static void SeedDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                try
                {
                    var movieLandContext = serviceProvider.GetRequiredService<MovieLandContext>();
                    MovieLandContextSeed.SeedAsync(movieLandContext, loggerFactory, roleManager, userManager).Wait();
                }
                catch (Exception exception)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(exception, "An error occurred seeding the DB.");
                }
            }
        }
    }
}