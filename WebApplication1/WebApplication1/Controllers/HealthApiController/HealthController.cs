using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.HealthApiController;

[ApiController]
[Route("api/[controller]")]
public class HealthController: ControllerBase
{
    [HttpGet]
    public IActionResult HealthCheck()
    {
        return Ok("API ativa");
    }
}