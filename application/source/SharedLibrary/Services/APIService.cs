using Blazor.Extensions;
using Newtonsoft.Json;
using SharedLibrary.Models.User;
using SharedLibrary.Models.User.Login.OpenId;
using SharedLibrary.Models.User.Login.OpenId.Token;
using System;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public class APIService
    {
        public APIService(HubConnectionBuilder hubConnectionBuilder)
        {
            this.HubConnection = this.Build(hubConnectionBuilder);
            this.Configure();
        }

        public HubConnection HubConnection { get; set; }
        public Action? ConnectedToAPIAction { get; set; } = null;
        public Action? ConnectedToDatabaseAction { get; set; } = null;

        private readonly string endpoint = "https://localhost:44300/API";
        private Task ConnectToAPITask;

        public void ConnectToAPIAsync()
        {
            this.ConnectToAPITask = Task.Run(async () => await this.HubConnection.StartAsync());
        }

        public async Task<UserBaseModel?> LoginUserWithOpenIdAsync(AuthenticationTokenOpenIdModel openIdAuthenticationTokenModel)
        {
            await this.ConnectToAPITask;
            if (this.ConnectToAPITask.IsCompleted)
            {
                UserBaseModel? user = await this.HubConnection.InvokeAsync<UserBaseModel?>("LoginUserWithOpenIdAsync", openIdAuthenticationTokenModel);

                if (user != null)
                {

                }
            }

            return null;
        }

        public async Task<OpenIdUrisModel?> GetOpenIdUrisAsync()
        {
            await this.ConnectToAPITask;
            if (this.ConnectToAPITask.IsCompleted)
            {
                return await this.HubConnection.InvokeAsync<OpenIdUrisModel?>("GetOpenIdUrisAsync");
            }

            return null;
        }

        private HubConnection Build(HubConnectionBuilder hubConnectionBuilder)
        {
            return this.HubConnection = hubConnectionBuilder
                    .WithUrl(endpoint, opt => {
                        opt.LogMessageContent = true;
                        opt.LogLevel = SignalRLogLevel.Trace; // Client log level
                    })
                    .Build();
        }

        private void Configure()
        {
            // Delega métodos para os eventos da conexão com o Hub
            this.HubConnection.On<string>("OnConnectedToAPI", (s) => this.OnConnectedToAPI());
            this.HubConnection.On<string>("OnConnectedToDatabase", (s) => this.OnConnectedToDatabase());
        }
        private async Task OnConnectedToAPI()
        {
            await Task.Run(() => { this.ConnectedToAPIAction?.Invoke(); });
        }
        private async Task OnConnectedToDatabase()
        {
            await Task.Run(() => { this.ConnectedToDatabaseAction?.Invoke(); });
        }

    }
}
