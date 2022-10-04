// ------------------------------------------------------------
// <copyright file="RegistrationUserCommandHandler.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Commands
{
    /// <summary>
    /// RegistrationUserCommandHandler.
    /// </summary>
    public class RegistrationUserCommandHandler : IRequestHandler<RegistrationUserCommand, int>
    {
        /// <summary>
        /// Chat database context.
        /// </summary>
        private readonly IChatDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationUserCommandHandler"/> class.
        /// </summary>
        /// <param name="context">IChatDbContext.</param>
        public RegistrationUserCommandHandler(IChatDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Handle.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Id.</returns>
        public async Task<int> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
        {
            var data = await this.context.Users.Where(u => u.Nickname.Equals(request.UserVm.Nickname)).FirstOrDefaultAsync();

            if (data != null)
            {
                throw new Exception($"User \"{request.UserVm.Nickname}\" already exist.");
            }

            User user = new User
            {
                Nickname = request.UserVm.Nickname,
                Password = request.UserVm.Password,
            };

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return user.Id;
        }
    }
}
