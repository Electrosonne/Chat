// ------------------------------------------------------------
// <copyright file="IChatDbContext.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chat.Application
{
    /// <summary>
    /// Chat database context.
    /// </summary>
    public interface IChatDbContext
    {
        /// <summary>
        /// Gets or sets users.
        /// </summary>
        DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets messages.
        /// </summary>
        DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Save changes asynchronously.
        /// </summary>
        /// <returns>Task.</returns>
        Task SaveChangesAsync();
    }
}
