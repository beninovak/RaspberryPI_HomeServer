using System.Net;
using HomeServer.Controllers;
using HomeServer.Services;

namespace HomeServer.Middleware;

public class SessionAuthenticationMiddleware(RequestDelegate next) {
    public async Task InvokeAsync(HttpContext context, IUserService userService) {
        var skipAuthAttribute = context.GetEndpoint()?.Metadata.GetMetadata<SkipAuthAttribute>();
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
        
        if (!await userService.IsSessionIdValidAsync(sessionId)) {
            // TODO - na frontendu redirectej na login page če response dobi 401
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        Guid userGuid = await userService.GetUserGuidBySessionIdAsync(sessionId);
        // V context.Items loh dam used guid. V controllerjih ga dobim iz HttpContext
        context.Items.Add("userGuid", userGuid);
        
        await next(context);
    }
}