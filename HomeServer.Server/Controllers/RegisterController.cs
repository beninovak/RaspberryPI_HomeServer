using HomeServer.Core.DTOs;
using HomeServer.Core.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using HomeServer.Core.Models;
using HomeServer.Server.Exceptions;
using HomeServer.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Controllers;

[Route("api/register")]
[ApiController]
public class RegisterController(IUserService userService) : ControllerBase {

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterDTO registerDto) {
        try {
            await userService.RegisterUserAsync(registerDto);
            return Ok(new {
                message = $"Successfully added a user with email {registerDto.Email}"
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
        } catch (DbUpdateException e) {
            return Conflict(new { // TODO - Prever čej to kul. Kva če ni conflict? Aj možn?
                message = e.Message,
            });
        }
    }
}