using HomeServer.DTOs.User;
using HomeServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HomeServer.Controllers;

[ApiController] [Route("api/Login")] [SkipAuth]
public sealed class LoginController(IUserService userService, IMemoryCache memoryCache) : ControllerBase {
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> LoginAsync([FromBody] LoginDTO user) {
        Guid userGuid = await userService.LoginUserAsync(user);
        Guid sessionId = Guid.NewGuid();
        SessionDataService sessionData = new SessionDataService {
            userGuid = userGuid,
            IP = HttpContext.Connection.RemoteIpAddress, // TODO - Baje je RemoteIp loh null...prever ta case
        };
        memoryCache.Set(sessionId, sessionData, DateTime.UtcNow.AddDays(7));
        
        return Ok(new {
            sessionId = sessionId
        });
    }
}