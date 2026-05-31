using HomeServer.DTOs.User;
using HomeServer.Services;
using Microsoft.AspNetCore.Mvc;
using HomeServer.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace HomeServer.Controllers;

[ApiController] [Route("api/login")] [SkipAuth]
public sealed class LoginController(IUserService userService, IMemoryCache memoryCache) : ControllerBase {
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> LoginAsync([FromBody] LoginDTO user) {
        try {
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
        } catch (AggregateException e) {
            return BadRequest(e.InnerExceptions);
        } catch (NonExistentUserException e) {
            return NotFound(new {
                message = e.Message,
            });
        } catch (UserInvalidLoginException e) {
            return Unauthorized(new {
                message = e.Message,
            });
        }
    }
}