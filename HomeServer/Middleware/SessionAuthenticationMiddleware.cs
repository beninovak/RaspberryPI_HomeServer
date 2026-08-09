using HomeServer.Controllers;
using HomeServer.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HomeServer.Middleware;

public class SessionAuthenticationMiddleware(RequestDelegate next, IMemoryCache memoryCache) {
    
    private Guid sessionId;
    private object? sessionDataObject;

    private const string defaultPage = "/todo";
    private readonly string[] invalidAuthPages = ["/Login", "/register"];
    private readonly string[] validNoAuthPages = ["/Login", "/register", "/forgotten-password"];
    
    public async Task InvokeAsync(HttpContext context, ISessionDataService sessionDataService) {
        bool hasValidCookie = HasValidCookie(context);
        
        string path = context.Request.Path.Value ?? "";
        
        // Ok condition za FE?
        if (!path.StartsWith("/api")) {
            if (hasValidCookie) {
                if (invalidAuthPages.Contains(path)) {
                    context.Response.Redirect(defaultPage);
                    return;
                }
                
                await next(context);
                return;
            }
            
            if (path == "/") {
                context.Response.Redirect("/Login");
                return;
            }

            if (!Path.HasExtension(path) && validNoAuthPages.Contains(path)) {
                context.Request.Path = path + ".html";
                await next(context);
                return;
            }
            
            if (Path.HasExtension(path) && !Path.GetExtension(path).Equals(".html")) {
                await next(context);
                return;
            }
            
            context.Response.StatusCode = 404;
            return;
        }

        // Server from here on
        if (hasValidCookie) {
            SessionDataService? sessionData = sessionDataObject as SessionDataService;
            if (sessionData is null || sessionData.IP is null || !sessionData.IP.Equals(context.Connection.RemoteIpAddress)) {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            sessionDataService.userGuid = sessionData.userGuid;
            //sessionDataService.IP = sessionData.IP;
            await next(context);
            return;
        }

        Endpoint? endpoint = context.GetEndpoint();
        SkipAuthAttribute? skipAuthAttribute = endpoint?.Metadata.GetMetadata<SkipAuthAttribute>();
        if (skipAuthAttribute is not null) {
            await next(context);
            return;
        }
        
        context.Response.StatusCode = 401; // Unauthorized
    }

    private bool HasValidCookie(HttpContext context) {
        if (!context.Request.Headers.ContainsKey("Cookie") || context.Request.Headers["Cookie"].Count == 0) {
            return false;
        }

        if (!Guid.TryParse(context.Request.Headers["Cookie"][0], out sessionId) || sessionId.Equals(Guid.Empty)) {
            return false;
        }

        if (!memoryCache.TryGetValue(sessionId, out sessionDataObject) || sessionDataObject is null) {
            return false;
        }
        
        return true;
    }
}