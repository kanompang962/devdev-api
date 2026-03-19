using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using devdev_api.Common;

namespace devdev_api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled: {Message}", ex.Message);
                await WriteErrorAsync(ctx, ex);
            }
        }

        private static Task WriteErrorAsync(HttpContext ctx, Exception ex)
        {
            var (code, message) = ex switch
            {
                KeyNotFoundException => (HttpStatusCode.NotFound,            ex.Message),
                ArgumentException    => (HttpStatusCode.BadRequest,          ex.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
                _                    => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };

            var response = ApiResponse<object>.Fail(message);

            ctx.Response.StatusCode  = (int)code;
            ctx.Response.ContentType = "application/json";

            return ctx.Response.WriteAsync(
                JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }));
        }
    }
}