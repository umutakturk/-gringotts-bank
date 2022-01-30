using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using GringottsBank.Common.Exceptions;
using GringottsBank.Common.Extensions;
using GringottsBank.Common.Models;
using GringottsBank.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ApplicationException = GringottsBank.Common.Exceptions.ApplicationException;

namespace GringottsBank.API.Infrastructure
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e) when (e is ApplicationException)
            {
                _logger.LogError(e, "BadRequest");
                await ExceptionResponse(httpContext, HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e) when (e is NotFoundException)
            {
                _logger.LogWarning(e, "NotFound");
                await ExceptionResponse(httpContext, HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e) when (e is DomainException || e is ConcurrencyException)
            {
                _logger.LogError(e, "Conflict");
                await ExceptionResponse(httpContext, HttpStatusCode.Conflict, e.Message);
            }
            catch (Exception e) when (e is ValidationException)
            {
                _logger.LogError(e, "UnprocessableEntity");
                var messages = (e as ValidationException).Errors.Select(s => s.ErrorMessage).ToArray();
                await ExceptionResponse(httpContext, HttpStatusCode.UnprocessableEntity, messages);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "InternalServerError");
                await ExceptionResponse(httpContext, HttpStatusCode.InternalServerError, e.Message);
            }
        }

        private static Task ExceptionResponse(HttpContext httpContext, HttpStatusCode statusCode, string message)
        {
            return ExceptionResponse(httpContext, statusCode, new[] { message });
        }

        private static async Task ExceptionResponse(HttpContext httpContext, HttpStatusCode statusCode, string[] messages)
        {
            var code = (int)statusCode;
            var error = Result.Failure<string>(messages);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = code;
            await httpContext.Response.WriteAsync(error.ToJson());
        }
    }
}
