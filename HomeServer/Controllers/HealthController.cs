using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Controllers;

[Route("api/health")]
public class HealthController : ControllerBase {

    [HttpGet]
    public OkObjectResult CheckHealth() {
        return Ok("I've server!");
    }
}