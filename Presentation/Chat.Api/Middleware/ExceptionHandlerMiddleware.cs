// ------------------------------------------------------------
// <copyright file="ExceptionHandlerMiddleware.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Exceprion Handler Middleware.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        /// <summary>
        /// Next layer in pipeline of handling request.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">Next request delegate.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Automatically invoked by framework.
        /// </summary>
        /// <param name="context">HttpContext of request.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                await this.HandleExceprionAsync(context, exception);
            }
        }

        /// <summary>
        /// Handle exceprion async.
        /// </summary>
        /// <param name="context">HttpContext of request.</param>
        /// <param name="exception">Exceprion.</param>
        /// <returns>Task.</returns>
        private Task HandleExceprionAsync(HttpContext context, Exception exception)
        {
            string result = JsonSerializer.Serialize(exception.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(result);
        }
    }
}
