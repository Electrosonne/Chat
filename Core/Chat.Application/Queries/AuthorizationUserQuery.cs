// ------------------------------------------------------------
// <copyright file="AuthorizationUserQuery.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Domain;
using MediatR;

namespace Chat.Application.Queries
{
    /// <summary>
    /// AuthorizationUserQuery.
    /// </summary>
    public class AuthorizationUserQuery : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets user.
        /// </summary>
        public User User { get; set; }
    }
}
