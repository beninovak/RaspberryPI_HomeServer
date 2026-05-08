using HomeServer.Core.DTOs;
using HomeServer.Core.Models;
using HomeServer.Server.Services;
using Microsoft.AspNetCore.Mvc;
using HomeServer.Server.Exceptions;

namespace HomeServer.Server.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController(IUserService userService) : ControllerBase {
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginDTO user) {
        try {
            await userService.LoginUser(user);
            return Ok(new {
                message = $"Email and password match. Logging in user - {user.Email}"
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