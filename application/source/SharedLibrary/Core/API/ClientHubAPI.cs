using Blazor.Extensions;
using System;
using System.Threading.Tasks;

namespace SharedLibrary.Core.API
{
    public class ClientHubAPI
    {
        //public SharedAPI(HubConnectionBuilder hubConnectionBuilder)
        //{
        //    HubConnectionBuilder = hubConnectionBuilder;
        //}

        public HubConnection? HubConnection { get; set; } = null;
        public HubConnectionBuilder? HubConnectionBuilder { get; set; } = null;
        public string Message { get; set; } = "";
        public string SerializedObject { get; set; } = "";

        public Action? ConnectedToAPIAction { get; set; } = null;
        public Action? ReceivedMessageAction { get; set; } = null;
        public Action? ReceivedObjectAction { get; set; } = null;
        public Action? ReceivedMessageWithObjectAction { get; set; } = null;

        private readonly string Endpoint = "https://localhost:44300/API";
        //private bool IsConnected = false;

        public async Task BuildAndConnectAsync(HubConnectionBuilder hubConnectionBuilder, string? endpoint = null)
        {
            if (HubConnection == null)
            {
                endpoint ??= this.Endpoint;

                HubConnection = hubConnectionBuilder
                        .WithUrl(endpoint, opt => {
                            opt.LogMessageContent = true;
                            opt.LogLevel = SignalRLogLevel.Trace; // Client log level
                        })
                        .Build();

                // Delega métodos para os eventos da conexão com o Hub
                HubConnection.On<string>("ConnectedToAPI", (message) => this.OnConnectedToAPI(message));
                HubConnection.On<string>("ReceiveMessage", (message) => this.OnResponseReceived(message));
                HubConnection.On<string>("ReceiveObject", (serializedObject) => this.OnObjectReceived(serializedObject));
                HubConnection.On<string, string>("ReceiveMessageWithObject", (message, serializedObject) => this.OnMessageWithObjectReceived(message, serializedObject));

                await HubConnection.StartAsync(); // Inicia a conexão com a API
            }
        }
        public async Task OnConnectedToAPI(string message)
        {
            Message = message;
            ConnectedToAPIAction?.Invoke();
        }

        public async Task  OnResponseReceived(string message)
        {
            Message = message;
            ReceivedMessageAction?.Invoke();
        }

        public async Task OnObjectReceived(string serializedObject)
        {
            SerializedObject = serializedObject;

            ReceivedObjectAction?.Invoke();
        }

        public async Task OnMessageWithObjectReceived(string message, string serializedObject)
        {
            Message = message;
            SerializedObject = serializedObject;

            ReceivedMessageWithObjectAction?.Invoke();
        }

        /// <summary>Conecta com o Endpoint padrão - /API - caso nenhum seja especificado</summary>
        /// <remarks>Uses polar coordinates</remarks>
        /// <param name="jsRuntime">O JSRuntime do Blazor (gerado após OnAfterRenderAsync)</param>
        /// <param name="endpoint">Endpoint da API - caso não utilizado, usa-se:<code>/API</code></param>
        //public async Task BuildAndConnectAsync(IJSRuntime jsRuntime, string? endpoint = null)
        //{
        //    if (HubConnection != null)
        //    {
        //        endpoint ??= this.Endpoint;
        //        //if (String.IsNullOrEmpty(endpoint))
        //        //{
        //        //    endpoint = this.Endpoint;
        //        //}
        //        HubConnection = new HubConnectionBuilder(jsRuntime)
        //                .WithUrl(endpoint, opt => {
        //                    opt.LogLevel = SignalRLogLevel.Trace; // Client log level
        //            })
        //                .Build();

        //        // Delega métodos para os eventos da conexão com o Hub
        //        HubConnection.On<string>("ReceiveMessage", (message) => this.OnResponseReceived(message));
        //        HubConnection.On<string>("ReceiveObject", (obj) => this.OnObjectReceived(obj));

        //        await HubConnection.StartAsync();
        //    }
        //}

        public async Task SendMessageAsync(string method, string message)
        {
            await HubConnection.InvokeAsync(method, message);
        }

        public async Task SendMessageAsync(string method)
        {
            await HubConnection.InvokeAsync(method);
        }
    }
}
