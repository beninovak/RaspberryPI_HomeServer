using HomeServer.Core;
using HomeServer.Server.Data;
using HomeServer.Server.Models;
using HomeServer.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase {
    private readonly MainDbContext DB;

    public LoginController(MainDbContext db) {
        DB = db;
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> AttemptUserLogin([FromBody] UserDTO userDTO) {
        var (isValidEmail, validationMessageEmail) = UserUtil.IsEmailValid(userDTO.Email);
        if (!isValidEmail) {
            return BadRequest(new {
                message = validationMessageEmail,
            });
        }

        var (isValidPassword, validationMessagePassword) = UserUtil.IsPasswordValid(userDTO.Email);
        if (!isValidPassword) {
            return BadRequest(new {
                message = validationMessagePassword,
            });
        }

        User? dbUser = await DB.Users.FirstOrDefaultAsync(u => u.Email.Equals(userDTO.Email));
        if (dbUser is null) {
            return NotFound(new {
                message = "No user with such email exists!"
            });
        }

        string hashedPassword = UserUtil.GenerateHashedPassword(userDTO.Password + dbUser.PasswordSalt);
        if (!hashedPassword.Equals(dbUser.Password)) {
            return Unauthorized(new {
                message = "Invalid password!"
            });
        }

        return Ok(new {
            message = $"Email and password match. Logging in user - {dbUser.Email}"
        });
    }
}