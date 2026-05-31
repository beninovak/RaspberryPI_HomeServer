using HomeServer.Controllers;
using HomeServer.Services;
using Microsoft.Extensions.Caching.Memory;
// ReSharper disable All

namespace HomeServer.Middleware;

public class SessionAuthenticationMiddleware(RequestDelegate next) {
    public async Task InvokeAsync(HttpContext context, IMemoryCache memoryCache, ISessionDataService sessionDataService) {
        Endpoint? endpoint = context.GetEndpoint();
        
        // Če je request za front end je endpoint null. Nevem zakva...mađik? Ig ker dojame da se path ne ujema s katerokol metodo v controllerjih??
        if (endpoint is null) {
            await next(context);
            return;
        }

        SkipAuthAttribute? skipAuthAttribute = endpoint.Metadata.GetMetadata<SkipAuthAttribute>();
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

        object? sessionDataObject;
        if (!memoryCache.TryGetValue(sessionId, out sessionDataObject) || sessionDataObject is null) {
            // TODO - na frontendu redirectej na login page če response dobi 401
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        SessionDataService? sessionData = sessionDataObject as SessionDataService;
        if (sessionData is null || sessionData.IP is null || !sessionData.IP.Equals(context.Connection.RemoteIpAddress)) {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        sessionDataService.userGuid = sessionData.userGuid;
        //sessionDataService.IP = sessionData.IP;
        await next(context);
    }
}