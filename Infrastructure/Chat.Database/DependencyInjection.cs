// ------------------------------------------------------------
// <copyright file="DependencyInjection.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Database
{
    /// <summary>
    /// Dependency injection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// AddApplication.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <returns>Services with MediatR.</returns>
        public static IServiceCollection AddDatabase(
           this IServiceCollection services)
        {
            services.AddSingleton<IChatDbContext, ChatDbContext>();
            return services;
        }
    }
}
