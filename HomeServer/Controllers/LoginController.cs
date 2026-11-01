using Shared.DTOs.User;
using HomeServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HomeServer.Controllers;

[ApiController] [Route("api/login")] [SkipAuth]
public sealed class LoginController(IUserService userService, IMemoryCache memoryCache) : ControllerBase {
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> LoginAsync([FromForm] LoginDTO user) {
        LoginResult loginResult = await userService.LoginUserAsync(user);

        if (!loginResult.Success) {
            return Unauthorized(loginResult.Errors);
        }
        
        Guid sessionId = Guid.NewGuid();
        SessionDataService sessionData = new SessionDataService {
            userGuid = loginResult.Guid!.Value,
            IP = HttpContext.Connection.RemoteIpAddress, // TODO - Baje je RemoteIp loh null...prever ta case
        };

        DateTime cookieExpirationDateTime = DateTime.UtcNow.AddDays(7);

        memoryCache.Set(sessionId, sessionData, cookieExpirationDateTime);

        var options = new CookieOptions {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = cookieExpirationDateTime,
        };
        Response.Cookies.Append("", sessionId.ToString(), options); // Automatically sets cookie in browser

        return Ok(new {
            sessionId = loginResult.Guid,
        });
    }
}