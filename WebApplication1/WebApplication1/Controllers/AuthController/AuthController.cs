using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers.AuthController;

[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = HttpContext.User;
        if (user?.Identity == null || !user.Identity.IsAuthenticated)
        {
            return Unauthorized(new { authenticated = false });
        }
        
        var roleActive = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        return Ok(new
        {
            authenticated = true,
            role = roleActive
        });
    }
}