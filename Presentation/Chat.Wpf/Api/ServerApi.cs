// ------------------------------------------------------------
// <copyright file="ServerApi.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using Chat.Domain;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat.Wpf
{
    /// <summary>
    /// Api for requests on server.
    /// </summary>
    public class ServerApi : IDisposable
    {
        /// <summary>
        /// Url of server.
        /// </summary>
        /// https://localhost:44321
        /// http://localhost:5000
        private const string Url = "https://localhost:44321";

        /// <summary>
        /// HttpClient.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// JsonSerializerOptions.
        /// </summary>
        private readonly JsonSerializerOptions jsonSerializerOptions;

        /// <summary>
        /// Server hub of messages.
        /// </summary>
        private HubConnection hubConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerApi"/> class.
        /// </summary>
        public ServerApi()
        {
            this.httpClient = new HttpClient();
            this.jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        /// <summary>
        /// Delegate of message.
        /// </summary>
        /// <param name="message">MessageVm.</param>
        public delegate void MessageDelegate(MessageVm message);

        /// <summary>
        /// Message received event.
        /// </summary>
        public event MessageDelegate MessageReceived;

        /// <summary>
        /// Initializes a HubConnection.
        /// </summary>
        public void StartHub()
        {
            this.hubConnection = new HubConnectionBuilder().WithUrl(Url + "/Chat").Build();
            this.hubConnection.On<MessageVm>("Send", message =>
            {
                this.MessageReceived.Invoke(message);
            });
            this.hubConnection.StartAsync();
        }

        /// <summary>
        /// Ping to server.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task<bool> Ping()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, Url + "/Ping");
                var response = await this.httpClient.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get 10 messages before date asynchronously.
        /// </summary>
        /// <param name="date">DateTime.</param>
        /// <returns>Task(IList(MessageVm)).</returns>
        public async Task<IList<MessageVm>> GetMessages(DateTime date)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url + "/GetMessages");
            request.Content = JsonContent.Create(date);
            var response = await this.httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<IList<MessageVm>>(responseStream, this.jsonSerializerOptions);
            return result;
        }

        /// <summary>
        /// Authorization asynchronously.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>Task(bool).</returns>
        public async Task<bool> Authorization(User user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url + "/Authorization");
            request.Content = JsonContent.Create(user);
            var response = await this.httpClient.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Registration asynchronously.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>Task(bool).</returns>
        public async Task<bool> Registration(User user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url + "/Registration");
            request.Content = JsonContent.Create(user);
            var response = await this.httpClient.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Send message to hub.
        /// </summary>
        /// <param name="message">MessageVm.</param>
        /// <returns>Task.</returns>
        public async Task SendMessage(MessageVm message)
        {
            this.HubCheck();

            await this.hubConnection.SendAsync("GetMessage", message);
        }

        /// <inheritdoc/>
        public async void Dispose()
        {
            this.httpClient.Dispose();
            await this.hubConnection.StopAsync();
        }

        /// <summary>
        /// Check hub connection.
        /// </summary>
        private async void HubCheck()
        {
            if (this.hubConnection.State == HubConnectionState.Disconnected)
            {
                await this.hubConnection.StartAsync();
            }
        }
    }
}
