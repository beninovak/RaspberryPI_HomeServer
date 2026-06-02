using HomeServer.Controllers;
using HomeServer.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HomeServer.Middleware;

public class SessionAuthenticationMiddleware(RequestDelegate next, IMemoryCache memoryCache) {
    
    private Guid sessionId;
    private object? sessionDataObject;
    
    private readonly string[] validNoAuthPages = ["/login", "/register", "/forgotten-password"];
    
    public async Task InvokeAsync(HttpContext context, ISessionDataService sessionDataService) {
        bool hasValidCookie = HasValidCookie(context);
        
        Endpoint? endpoint = context.GetEndpoint();
        // Endpoint je null, ko je request iz FE. Mađik?
        if (endpoint is null) {
            if (hasValidCookie) {
                await next(context);
                return;
            }
            
            string path = context.Request.Path.Value ?? "";
            if (path == "/") {
                context.Response.Redirect("/login");
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
        
        SkipAuthAttribute? skipAuthAttribute = endpoint.Metadata.GetMetadata<SkipAuthAttribute>();
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