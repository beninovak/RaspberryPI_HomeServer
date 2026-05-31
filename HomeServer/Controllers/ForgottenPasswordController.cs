using HomeServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Controllers;

[ApiController] [Route("api/forgotten-password")] [SkipAuth]
public sealed class ForgottenPasswordController(IUserRepository userRepository) : ControllerBase {
    
    [HttpPost]
    public async Task<ActionResult> SendForgottenPasswordEmailAsync([FromBody] Guid guid) {
        // TODO - send email
        //return Accepted($"Sending password reset email to {email}.");
        return RedirectPermanent("Se ga enkat drugič"); // TODO - kle vrn forgotten password url na FE
    }
}