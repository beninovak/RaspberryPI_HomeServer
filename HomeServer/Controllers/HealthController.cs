using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Controllers;

[Route("api/health")] [SkipAuth]
public sealed class HealthController : ControllerBase {

    [HttpGet]
    public OkObjectResult CheckHealth() {
        return Ok("I've server!");
    }
}