using DientesLimpios.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace DientesLimpios.API.Middleware
{
    public class ManejadorExceptionesMiddleware
    {
        private readonly RequestDelegate _next;

        public ManejadorExceptionesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Inkoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManejarExcepcion(context, ex);
            }
        }

        private Task ManejarExcepcion(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var resultado = string.Empty;

            switch (exception)
            {
                case NoEncontradoException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ValidacionException validacionException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    resultado = JsonSerializer.Serialize(validacionException.Errors);
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(resultado);
        }

    }
    public static class ManejadorExceptionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseManejadorExceptiones(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManejadorExceptionesMiddleware>();
        }
    }
}