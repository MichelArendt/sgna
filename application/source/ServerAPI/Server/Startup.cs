using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerAPI.Server.Core.API;
using ServerAPI.Server.Database;
using ServerAPI.Server.Services;
using System.Globalization;
using System.Linq;

namespace ServerAPI.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()//WithOrigins("https://localhost:44301")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                       //.AllowCredentials();
            }));
            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            // Serviços .NET Core
            services.AddSignalR();

            // Devem ser carregados primeiro
            services.AddSingleton<AppSettingsService>();

            // Build an intermediate service provider to get builderOptions for AddDbContextPool
            AppSettingsService appSettingsService =
                services.BuildServiceProvider().GetService<AppSettingsService>();

            services.AddDbContext<DatabaseContext>(
                options => appSettingsService.GetDatabaseOptions());
            services.AddScoped<DatabaseService>();

            services.AddSingleton<OpenIdSettingsService>();
            services.AddSingleton<ClientSecretsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppSettingsService appSettingsService = app.ApplicationServices.GetService<AppSettingsService>();
            CultureInfo cultureInfo = new CultureInfo(appSettingsService.AppSettings.DefaultLanguage);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            // Permite que o cliente conecte ao servidor API
            app.UseCors("MyPolicy");

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
                endpoints.MapHub<ServerAPICore>("/API");
                //endpoints.MapHub<ServerHubAPI>("/API");
                //endpoints.MapHub<LoginServerAPI>("/API/Login");
            });
        }
    }
}
