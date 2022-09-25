// ------------------------------------------------------------
// <copyright file="HomeController.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using Chat.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Chat database context.
        /// </summary>
        private readonly IChatDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="context">Chat database context.</param>
        public HomeController(ILogger<HomeController> logger, IChatDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// Get 10 messages before date time from database.
        /// </summary>
        /// <param name="date">DateTime.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetMessages")]
        public IActionResult GetMessages([FromBody] DateTime date)
        {
            return this.Ok(this.context.Messages.Where(m => m.Date < date).Include(u => u.User).AsEnumerable().TakeLast(10).ToList());
        }

        /// <summary>
        /// Authorization.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("Authorization")]
        public async Task<IActionResult> Authorization([FromBody] User user)
        {
            var data = await this.context.Users.Where(u => u.Nickname.Equals(user.Nickname)).FirstOrDefaultAsync();

            if (data == null)
            {
                return this.BadRequest($"No finded user \"{user.Nickname}\".");
            }

            if (data.Password.Equals(user.Password))
            {
                return this.Ok();
            }
            else
            {
                return this.BadRequest($"Invalid password.");
            }
        }

        /// <summary>
        /// Authorization.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] User user)
        {
            var data = await this.context.Users.Where(u => u.Nickname.Equals(user.Nickname)).FirstOrDefaultAsync();

            if (data != null)
            {
                return this.BadRequest($"User \"{user.Nickname}\" already exist.");
            }

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return this.Ok();
        }

        /// <summary>
        /// Ping.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return this.Ok();
        }
    }
}
