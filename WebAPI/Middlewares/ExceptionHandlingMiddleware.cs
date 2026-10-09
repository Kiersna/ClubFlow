using System.Net;

namespace WebAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next; // ta linijaka jest zeby miec ten next do middlewarea 
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; // ta linijka jest po to zeby miec logger czyli zeby wypisac jaki to blad 
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response= new { message = "Wystąpił wewnętrzny błąd serwera." };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
