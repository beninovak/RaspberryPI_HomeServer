using System.Security.Cryptography;
using System.Text;
using HomeServer.Core;
using HomeServer.Server.Data;
using HomeServer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MainDbContext DB;

        public LoginController(MainDbContext db)
        {
            DB = db;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> AttemptUserLogin([FromBody] UserDTO userDTO)
        {
            if (String.IsNullOrEmpty(userDTO.Email)) {
                return BadRequest(new {
                    message = "No email provided in request!",
                });
            }
            if (String.IsNullOrEmpty(userDTO.Password)) {
                return BadRequest(new {
                    message = "No password provided in request!",
                });
            }
            // TODO - Add FluentValidator?? for email and password?

            User? dbUser = await DB.Users.FirstOrDefaultAsync(u => u.Email.Equals(userDTO.Email));
            if (dbUser is null) {
                return NotFound(new {
                    message = "No user with such email exists!", 
                });
            }
            
            SHA256 sha256 = SHA256.Create();
            UTF8Encoding utf8 = new UTF8Encoding();
            // TODO - Maybe make sure userDTO.Password and dbUser.PasswordSalt are not null?
            byte[] hashedPassword = sha256.ComputeHash(utf8.GetBytes(userDTO.Password + dbUser.PasswordSalt));

            if (!hashedPassword.SequenceEqual(utf8.GetBytes(dbUser.Password))) {
                return Unauthorized(new {
                    message = "Invalid password!",
                });
            }

            return Ok(new {
                message = $"Email and password match. Logging in user - {dbUser.Email}",
            });
        }
    }
}
