using HomeServer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using HomeServer.Services;

namespace HomeServer.Controllers;

[ApiController] [Route("api/register")] [SkipAuth]
public sealed class RegisterController(IUserService userService) : ControllerBase {

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> RegisterAsync([FromForm] RegisterDTO registerDto) {
        RegisterResult registerResult = await userService.RegisterUserAsync(registerDto);

        if (!registerResult.Success) {
            return BadRequest(registerResult.Errors);
        }
        
        return Redirect("/login");
    }
}