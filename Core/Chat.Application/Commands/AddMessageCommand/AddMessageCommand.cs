// ------------------------------------------------------------
// <copyright file="AddMessageCommand.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using MediatR;

namespace Chat.Application.Commands
{
    /// <summary>
    /// AddMessageCommand.
    /// </summary>
    public class AddMessageCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public MessageVm Message { get; set; }
    }
}
