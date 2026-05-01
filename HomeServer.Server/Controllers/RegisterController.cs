using Microsoft.AspNetCore.Mvc;
using HomeServer.Server.Data;
using HomeServer.Core;
using HomeServer.Core.Models;
using HomeServer.Server.Utils;

namespace HomeServer.Server.Controllers;

[Route("api/register")]
[ApiController]
public class RegisterController(MainDbContext db) : ControllerBase {

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> AttemptUserRegister([FromBody] User userDTO) {
        (bool isValidEmail, string validationMessageEmail) = UserUtil.IsEmailValid(userDTO.Email);
        if (!isValidEmail) {
            return BadRequest(new {
                message = validationMessageEmail,
            });
        }
        if (db.Users.Any(user => user.Email.Equals(userDTO.Email))) {
            return BadRequest(new {
                message = "A user with this email already exists!"
            });
        }

        if (string.IsNullOrEmpty(userDTO.FirstName) || string.IsNullOrEmpty(userDTO.LastName)) {
            return BadRequest(new {
                message = "You must enter both a first and last name!"
            });
        }

        (bool isValidPassword, string validationMessagePassword) = UserUtil.IsPasswordValid(userDTO.Email);
        if (!isValidPassword) {
            return BadRequest(new {
                message = validationMessagePassword,
            });
        }

        string passwordSalt = UserUtil.GeneratePasswordSalt(Constants.PASSWORD_SALT_LENGTH);
        string hashedPassword = UserUtil.GenerateHashedPassword(userDTO.Password);

        db.Users.Add(new User {
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Email = userDTO.Email,
            Password = hashedPassword,
            PasswordSalt = passwordSalt,
            CreatedAt = DateTime.Now,
        });
        await db.SaveChangesAsync();

        return Ok(new { 
            message = $"Successfully added a user with email {userDTO.Email}"
        });
    }
}