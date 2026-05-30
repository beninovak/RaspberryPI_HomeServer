using HomeServer.Controllers;
using HomeServer.Services;
using Microsoft.Extensions.Caching.Memory;
// ReSharper disable All

namespace HomeServer.Middleware;

public class SessionAuthenticationMiddleware(RequestDelegate next) {
    public async Task InvokeAsync(HttpContext context, IMemoryCache memoryCache, ISessionService sessionService) {
        Endpoint? endpoint = context.GetEndpoint();
        
        // Če je request za front end je endpoint null. Nevem zakva...mađik? Ig ker dojame da se path ne ujema s katerokol metodo v controllerjih??
        if (endpoint is null) {
            await next(context);
            return;
        }

        var skipAuthAttribute = endpoint.Metadata.GetMetadata<SkipAuthAttribute>();
        if (skipAuthAttribute is not null) {
            await next(context);
            return;
        }
        
        if (!context.Request.Headers.ContainsKey("Cookie") || context.Request.Headers["Cookie"].Count == 0) {
            context.Response.StatusCode = 400;
            return;
        }
        
        Guid sessionId;
        if (!Guid.TryParse(context.Request.Headers["Cookie"][0], out sessionId) || sessionId.Equals(Guid.Empty)) {
            context.Response.StatusCode = 400; // Bad request - in case of invalid guid format
            return;
        }

        Guid sessionUserGuid;
        if (!memoryCache.TryGetValue(sessionId, out sessionUserGuid)) {
            // TODO - na frontendu redirectej na login page če response dobi 401
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        sessionService.userGuid = sessionUserGuid;
        // TODO - uzem IP iz context.Connection
        await next(context);
    }
}