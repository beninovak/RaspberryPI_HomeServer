using System.Security.Cryptography;
using HomeServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Controllers;

[Route("api/forgotten-password")]
[ApiController]
public class ForgottenPasswordController(IUserRepository userRepository) : ControllerBase {
    
    [HttpGet("{guid:guid}")]
    public async Task<ActionResult> SendForgottenPasswordEmailAsync([FromRoute] Guid guid) {
        string? email = await userRepository.GetUserByGuidAsync(guid);
        // TODO - send email

        return Accepted($"Sending password reset email to {email}.");
    }

    // [HttpPost]
    // public async Task<ActionResult> VerifyPasswordResetCodeAsync([FromBody] string email) {
    //     
    // }
}