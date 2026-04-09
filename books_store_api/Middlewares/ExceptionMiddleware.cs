using books_store_BLL.Dtos.Services;
using System.Text;

namespace books_store_api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               
                //request
                await _next(context);
                //response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ServiceResponse response = new ServiceResponse
                {
                    Success = false,
                    Message = ex.Message,
            
                };
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
