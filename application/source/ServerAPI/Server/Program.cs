using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServerAPI.Server.Database;
using ServerAPI.Server.Services;
using System;

namespace ServerAPI.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            // Builds services at Startup.cs
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<DatabaseContext>();
                    var appSettings = services.GetRequiredService<AppSettingsService>();
                    //var openIdSettings = services.GetRequiredService<OpenIdSettingsService>();
                    if (dbContext.Database.EnsureCreated())
                    {
                        SeedData seed = new SeedData(dbContext, appSettings);
                    }
                    // TODO: context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            host.Run();
        } 
        
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseConfiguration(new ConfigurationBuilder()
                            .AddJsonFile("hosting.json", optional: false, reloadOnChange: true)
                            .AddJsonFile("open_id_settings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile("client_secrets.json", optional: false, reloadOnChange: true)
                            .AddCommandLine(args)
                            .Build())
                        .UseStartup<Startup>();
                });
            return host;
        }
    }
}
