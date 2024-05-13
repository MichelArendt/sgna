using Blazor.Extensions;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Models.Settings;
using SharedLibrary.Services;
using System.Globalization;

namespace ServerAPI.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<HubConnectionBuilder>(); // Adiciona BlazorExtensions.SignalR

            // Devem ser carregados primeiro
            services.AddSingleton<UserSettingsService>(); // Antes do DateTimeService e Localization
            services.AddSingleton<DateTimeService>();
            UserSettingsService userSettingsService = new UserSettingsService();
            services.AddSingleton(new LocalizationService(userSettingsService.CultureInfo));

            services.AddScoped<SessionService>();
            services.AddScoped<UserInterfaceService>();
            services.AddScoped<ActionCenterService>();
            services.AddScoped<ActionBarService>();
            services.AddScoped<APIService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
