// ------------------------------------------------------------
// <copyright file="AddMessageCommandHandler.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Commands
{
    /// <summary>
    /// AddMessageCommandHandler.
    /// </summary>
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, bool>
    {
        /// <summary>
        /// Chat database context.
        /// </summary>
        private readonly IChatDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddMessageCommandHandler"/> class.
        /// </summary>
        /// <param name="context">IChatDbContext.</param>
        public AddMessageCommandHandler(IChatDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Handle.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Id.</returns>
        public async Task<bool> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            Message message = new Message()
            {
                Date = request.Message.Date,
                Text = request.Message.Text,
            };

            var user = this.context.Users.Where(u => u.Nickname.Equals(request.Message.User.Nickname)).FirstOrDefault();
            message.User = user;
            this.context.Messages.Add(message);
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
