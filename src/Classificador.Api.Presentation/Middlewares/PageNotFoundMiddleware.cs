using System.Net;

namespace Classificador.Api.Presentation.Middlewares;

internal sealed class PageNotFoundMiddleware
{
    private readonly RequestDelegate _next;

    public PageNotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
        {
            context.Request.Path = "/Home/PageNotFound";
            await _next(context);
        }
    }
}