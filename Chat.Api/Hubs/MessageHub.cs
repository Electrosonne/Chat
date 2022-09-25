// ------------------------------------------------------------
// <copyright file="MessageHub.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using Chat.Domain;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Hubs
{
    /// <summary>
    /// Aggregates all messages.
    /// </summary>
    public class MessageHub : Hub, IDisposable
    {
        /// <summary>
        /// Chat database context.
        /// </summary>
        private readonly IChatDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHub"/> class.
        /// </summary>
        /// <param name="context">Chat database context.</param>
        public MessageHub(IChatDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Task.</returns>
        public Task GetMessage(Message message)
        {
            var user = this.context.Users.Where(u => u.Nickname.Equals(message.User.Nickname)).FirstOrDefault();
            message.User = user;
            this.context.Messages.Add(message);
            this.context.SaveChangesAsync();
            return this.Clients.Others.SendAsync("Send", message);
        }
    }
}
