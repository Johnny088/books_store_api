namespace books_store_api.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //request

            List<string> headers = new List<string>();
            foreach (var header in context.Request.Headers)
            {
                headers.Add($"{header.Key}: {header.Value}"); 
            }

            var startTime = DateTime.UtcNow;
            _logger.LogInformation($"Request start time: {DateTime.UtcNow}");
            _logger.LogInformation($"Method: {context.Request.Method}" +
                $"Path: {context.Request.Path}" +
                $"{string.Join("\n", headers)}");

            await _next(context);

            //response
            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;
            _logger.LogInformation($"response end time: {DateTime.UtcNow}");
            _logger.LogInformation($"duration time from query to response is: {duration.TotalMilliseconds}");
            _logger.LogInformation($"Status code: {context.Response.StatusCode}");
        }
    }
}
