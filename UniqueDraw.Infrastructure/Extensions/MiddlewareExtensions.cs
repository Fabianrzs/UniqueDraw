using Microsoft.AspNetCore.Builder;
using UniqueDraw.Infrastructure.Middleware;

namespace UniqueDraw.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static void UseExceptionMiddlewareApp(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
