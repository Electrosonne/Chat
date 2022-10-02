// ------------------------------------------------------------
// <copyright file="GetMessagesQuery.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using MediatR;
using System;
using System.Collections;

namespace Chat.Application.Queries
{
    /// <summary>
    /// GetMessagesQuery.
    /// </summary>
    public class GetMessagesQuery : IRequest<IList>
    {
        /// <summary>
        /// Gets or sets date time.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
