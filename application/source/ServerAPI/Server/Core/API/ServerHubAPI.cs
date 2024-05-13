using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ServerAPI.Server.Core.API
{
    public class ServerHubAPI : Hub
    {
        // Hubs are transient:

        // Don't store state in a property on the hub class. Every hub method call is executed on a new hub instance.
        // Use await when calling asynchronous methods that depend on the hub staying alive.For example, a method such as Clients.All.SendAsync(...) can fail if it's called without await and the hub method completes before SendAsync finishes.

        public ServerHubAPI()
        {
        }

        /// <summary>
        /// Evento executado quando usuário conecta-se a API
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("OnConnectedToAPI");
            await base.OnConnectedAsync();
        }

        public Task SendNotificationAllClientsAsync(string message)
        {
            return Clients.All.SendAsync("ReceiveNotification", message);
        }

        public Task SendNotificationCallerAsync(string message)
        {
            return Clients.Caller.SendAsync("ReceiveNotification", message);
        }

        public Task SendMessageToCallerAsync(string message, string method = "ReceiveMessage")
        {
            return Clients.Caller.SendAsync(method, message);
        }

        public Task SendObjectToCallerAsync(string serializedObject, string method = "ReceiveObject")
        {
            return Clients.Caller.SendAsync(method, serializedObject);
        }

        public Task SendMessageWithObjectToCallerAsync(string message, string serializedObject, string method = "ReceiveMessageWithObject")
        {
            return Clients.Caller.SendAsync(method, message, serializedObject);
        }
    }
}
