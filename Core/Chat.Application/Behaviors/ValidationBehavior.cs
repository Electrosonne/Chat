// ------------------------------------------------------------
// <copyright file="ValidationBehavior.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application
{
    /// <summary>
    /// ValidationBehavior.
    /// </summary>
    /// <typeparam name="TRequest">Request.</typeparam>
    /// <typeparam name="TResponse">Response.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Validators.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">Validators.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var errors = this.validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors).Where(failure => failure != null).ToList();

            if (errors.Count != 0)
            {
                throw new ValidationException(errors[0].ErrorMessage);
            }

            return next();
        }
    }
}
