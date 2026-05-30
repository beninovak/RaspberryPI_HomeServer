using HomeServer.DTOs.User;
using HomeServer.Services;
using Microsoft.AspNetCore.Mvc;
using HomeServer.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace HomeServer.Controllers;

[ApiController] [Route("api/login")] [SkipAuth]
public sealed class LoginController(IUserService userService, IMemoryCache memoryCache, ISessionService sessionService) : ControllerBase {
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> LoginAsync([FromBody] LoginDTO user) {
        try {
            Guid userGuid = await userService.LoginUserAsync(user);
            Guid sessionId = Guid.NewGuid();
            memoryCache.Set(sessionId, userGuid, DateTime.UtcNow.AddDays(7));
            return Ok(sessionId);
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