// ------------------------------------------------------------
// <copyright file="GetMessagesQueryHandler.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Queries
{
    /// <summary>
    /// GetMessagesQueryHandler.
    /// </summary>
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IList>
    {
        /// <summary>
        /// Chat database context.
        /// </summary>
        private readonly IChatDbContext context;

        /// <summary>
        /// Mapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMessagesQueryHandler"/> class.
        /// </summary>
        /// <param name="context">IChatDbContext.</param>
        /// <param name="mapper">Mapper.</param>
        public GetMessagesQueryHandler(IChatDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handle.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Id.</returns>
        public async Task<IList> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            List<MessageVm> list = new List<MessageVm>();

            foreach (var message in this.context.Messages.Where(m => m.Date < request.DateTime).Include(u => u.User).AsEnumerable().TakeLast(10).ToList())
            {
                list.Add(this.mapper.Map<MessageVm>(message));
            }

            return list;
        }
    }
}
