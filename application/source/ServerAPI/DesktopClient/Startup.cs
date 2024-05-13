using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerAPI.DesktopClient.Services;
using SharedLibrary.Services;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DesktopClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSignalR();

            // Serviços nossos
            services.AddSingleton<ConfigurationService>(); // Esse sempre em primeiro
            services.AddSingleton<DesktopWindowService>();
            services.AddSingleton(new LocalizationService(new CultureInfo("en-US"), "ServerAPI.DesktopClient.Resources.Languages", typeof(DesktopWindowService).Assembly));
            services.AddScoped<SessionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapHub<ChatHub>("/chatHub");
            });

            // Verifica se estamos em modo de desenvolvimento e se o software está sendo executado no navegador
            ConfigurationService configService = app.ApplicationServices.GetService<ConfigurationService>();
            if ( !(configService.IsDevelopment && configService.DebugExecutingAs().Equals("Browser")) )
            {
                // Cria a janela do Electron
                BrowserWindow window;

                Task createWindowTask = Task.Run(
                    async () => window = await Electron.WindowManager.CreateWindowAsync(
                        new BrowserWindowOptions
                        {
                            Width = 800,
                            MinWidth = 300,
                            Height = 600,
                            Show = true,
                            BackgroundColor = "#1e1e1e",
                            Center = true,
                            Frame = false,
                            Fullscreen = false
                        })
                );

                // Aguarda a criação da janela de forma sincrôna
                createWindowTask.Wait();

                // Se a janela foi criada com sucesso, a captura e cria o ícone do Tray
                if (createWindowTask.IsCompletedSuccessfully)
                {
                    BrowserWindow desktopWindow = Electron.WindowManager.BrowserWindows.FirstOrDefault();
                    desktopWindow.WebContents.Session.ClearCacheAsync();

                    MenuItem menu = new MenuItem
                    {
                        Label = "Exit!"
                    };

                    Electron.Tray.Show("wwwroot/images/system/icon_system_tray.png", menu);

                    // Guarda a janela do Electron para uso futuro no serviço de gerenciamento da janela
                    app.ApplicationServices.GetService<DesktopWindowService>().DesktopWindow = desktopWindow;
                }
            }
        }
    }
}
