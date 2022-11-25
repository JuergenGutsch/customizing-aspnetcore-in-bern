using System.Diagnostics;

namespace MiddlewareSample;

public class StopwatchMiddleware
{
    private readonly RequestDelegate _next;

    public StopwatchMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var s = new Stopwatch();
        s.Start();

        await _next(context);

        s.Stop();
        var result = s.ElapsedMilliseconds;

        await context.Response.WriteAsync($"Time needed: {result}");
    }
}

public static class StopwatchMiddlewareExtension
{
    public static IApplicationBuilder UseStopwatch(
        this IApplicationBuilder app)
    {

        app.UseMiddleware<StopwatchMiddleware>();

        return app;
    }
}