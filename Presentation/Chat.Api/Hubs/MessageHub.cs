// ------------------------------------------------------------
// <copyright file="MessageHub.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using Chat.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
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
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHub"/> class.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public MessageHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Task.</returns>
        public async Task GetMessage(MessageVm message)
        {
            AddMessageCommand command = new AddMessageCommand() { Message = message };
            await this.mediator.Send(command);
            await this.Clients.Others.SendAsync("Send", message);
        }
    }
}
