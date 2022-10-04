// ------------------------------------------------------------
// <copyright file="AuthorizationUserQueryHandler.cs" company="ElectroSonne">
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

namespace Chat.Application.Queries
{
    /// <summary>
    /// AuthorizationUserQueryHandler.
    /// </summary>
    public class AuthorizationUserQueryHandler : IRequestHandler<AuthorizationUserQuery, bool>
    {
        /// <summary>
        /// Chat database context.
        /// </summary>
        private readonly IChatDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationUserQueryHandler"/> class.
        /// </summary>
        /// <param name="context">IChatDbContext.</param>
        public AuthorizationUserQueryHandler(IChatDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Handle.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Id.</returns>
        public async Task<bool> Handle(AuthorizationUserQuery request, CancellationToken cancellationToken)
        {
            UserVm user = request.UserVm;

            var data = await this.context.Users.Where(u => u.Nickname.Equals(user.Nickname)).FirstOrDefaultAsync();

            if (data == null)
            {
                throw new Exception($"No finded user \"{user.Nickname}\".");
            }

            if (data.Password.Equals(user.Password))
            {
                return true;
            }
            else
            {
                throw new Exception($"Invalid password.");
            }
        }
    }
}
