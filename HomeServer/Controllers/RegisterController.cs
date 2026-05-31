using HomeServer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using HomeServer.Services;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Controllers;

[ApiController] [Route("api/register")] [SkipAuth]
public sealed class RegisterController(IUserService userService) : ControllerBase {

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterDTO registerDto) {
        await userService.RegisterUserAsync(registerDto);
        return Ok(new {
            message = $"Successfully added a user with email {registerDto.Email}"
        });
    }
}