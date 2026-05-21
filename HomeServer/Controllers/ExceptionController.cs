using System.Security.Cryptography;
using HomeServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Controllers;

[ApiController]
public class ExceptionController : ControllerBase {
    
    [Route("/exceptions")] [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> HandleExceptionsAsync() {
        return Problem();
    }
}