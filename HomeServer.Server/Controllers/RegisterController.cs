using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using HomeServer.Server.Data;
using HomeServer.Core;
using HomeServer.Server.Models;
using HomeServer.Server.Utils;

namespace HomeServer.Server.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MainDbContext DB;

        public RegisterController(MainDbContext db)
        {
            DB = db;
        }

        // POST: api/register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AttempUserRegister([FromBody] UserDTO userDTO) {
            if (String.IsNullOrEmpty(userDTO.Email)) {
                return BadRequest(new {
                    message = "No email provided in request!",
                });
            } /*else if (!IsEmailValid(UserDTO.Email)) {
                // TODO - Also add FluentValidator here
                // Not valid error goes here
            } else if (DB.Users.Any(user => user.Email.Equals(userDTO.Email))) {
                // User with this email already exists
            }*/
            
            if (String.IsNullOrEmpty(userDTO.FirstName) || String.IsNullOrEmpty(userDTO.LastName)) {
                return BadRequest(new {
                    message = "You must enter both a first and last name!",
                });
            }
            
            if (String.IsNullOrEmpty(userDTO.Password)) {
                return BadRequest(new {
                    message = "No password provided in request!",
                });
            } /*else if (IsPasswordValid(UserDTO.Email)) {
                // TODO - Also add FluentValidator here
            }*/
            
            
            // TODO - Baje bi mogu uporablat PasswordHasher??
            // https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-10.0
            SHA256 sha256 = SHA256.Create();
            UTF8Encoding utf8 = new UTF8Encoding();
            string passwordSalt = UserUtil.GeneratePasswordSalt(12);
                
            // TODO - Maybe make sure userDTO.Password and dbUser.PasswordSalt are not null?
            string hashedPassword = utf8.GetString(sha256.ComputeHash(utf8.GetBytes(userDTO.Password + passwordSalt)));
            
            // TODO - Validate hashedPassword? How tho?
            
            DB.Users.Add(new User() {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = hashedPassword,
                PasswordSalt = passwordSalt,
            });
            await DB.SaveChangesAsync();
            
            return Ok(new { // TODO - Baje bi mogu uporabt CreatedAtRoute()??? Check...
                message = $"Successfully added a user with email {userDTO.Email}",
            });
        }
    }
}
