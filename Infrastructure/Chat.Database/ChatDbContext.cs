// ------------------------------------------------------------
// <copyright file="ChatDbContext.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chat.Database
{
    /// <summary>
    /// Chat database context implementation.
    /// </summary>
    public class ChatDbContext : DbContext, IChatDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatDbContext"/> class.
        /// </summary>
        public ChatDbContext()
        {
            this.Database.EnsureCreated();
        }

        /// <inheritdoc/>
        public DbSet<User> Users { get; set; }

        /// <inheritdoc/>
        public DbSet<Message> Messages { get; set; }

        /// <inheritdoc/>
        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        /// <summary>
        /// Configurating builder.
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=chat;Trusted_Connection=True;");
        }
    }
}
