// ------------------------------------------------------------
// <copyright file="HomeController.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using AutoMapper;
using Chat.Application.Commands;
using Chat.Application.Queries;
using Chat.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        /// Mapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Mediator.
        /// </summary>
        private IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="mediator">Mediator.</param>
        /// <param name="mapper">Mapper.</param>
        public HomeController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get 10 messages before date time from database.
        /// </summary>
        /// <param name="date">DateTime.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages([FromBody] DateTime date)
        {
            GetMessagesQuery command = new GetMessagesQuery() { DateTime = date };
            return this.Ok(await this.mediator.Send(command));
        }

        /// <summary>
        /// Authorization.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("Authorization")]
        public async Task<IActionResult> Authorization([FromBody] User user)
        {
            AuthorizationUserQuery command = new AuthorizationUserQuery() { User = user };
            return this.Ok(await this.mediator.Send(command));
        }

        /// <summary>
        /// Authorization.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] User user)
        {
            RegistrationUserCommand command = new RegistrationUserCommand() { User = user };
            return this.Ok(await this.mediator.Send(command));
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
