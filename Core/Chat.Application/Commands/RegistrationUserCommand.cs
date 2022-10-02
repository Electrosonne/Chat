﻿// ------------------------------------------------------------
// <copyright file="RegistrationUserCommand.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Domain;
using MediatR;

namespace Chat.Application.Commands
{
    /// <summary>
    /// RegistrationUserCommand.
    /// </summary>
    public class RegistrationUserCommand : IRequest<int>
    {
        /// <summary>
        /// Gets or sets user.
        /// </summary>
        public User User { get; set; }
    }
}