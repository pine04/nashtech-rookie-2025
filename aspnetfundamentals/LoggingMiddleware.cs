public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        DateTime requestTimestamp = DateTime.Now;

        string method = context.Request.Method;
        string scheme = context.Request.Scheme;
        string host = context.Request.Host.Value;
        string path = context.Request.Path.Value ?? string.Empty;
        string queryString = context.Request.QueryString.Value ?? string.Empty;

        using StreamReader reader = new StreamReader(context.Request.Body);
        string body = await reader.ReadToEndAsync();

        string log = $"[{requestTimestamp:dd/MM/yyyy HH:mm:ss}] {method} {scheme}://{host}{path}{queryString}{Environment.NewLine}{body}{Environment.NewLine}";

        File.AppendAllText("logs.txt", log);

        await _next(context);
    }
}

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}