using System.Net;
using System.Text.Json;

namespace Questao5.Infrastructure;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception error)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";

            if (error.GetType() == typeof(CustomErrorException) || error.InnerException.GetType() == typeof(CustomErrorException))
            {
                List<string> mensagens;

                if (error.GetType() == typeof(CustomErrorException))
                    mensagens = ((CustomErrorException)error).Notificacoes;
                else
                    mensagens = ((CustomErrorException)error.InnerException).Notificacoes;

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                string result = SerializarRetorno(mensagens);
                await response.WriteAsync(result);
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                List<string> errors = new();
                errors.Add(error.Message ?? "");
                if (!string.IsNullOrEmpty(error.StackTrace))
                    errors.Add(error.StackTrace);
                if (error.InnerException is not null)
                    errors.Add(error.InnerException.Message);

                string result = SerializarRetorno(errors);
                await response.WriteAsync(result);
            }


        }
    }

    private static string SerializarRetorno(List<string> notificacoes) => JsonSerializer.Serialize(new CustomResponse(notificacoes, false));
}

public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<ErrorMiddleware>();
}
