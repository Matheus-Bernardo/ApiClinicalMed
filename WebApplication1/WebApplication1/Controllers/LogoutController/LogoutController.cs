using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers.LogoutController;


[ApiController]
[Route("api/[controller]")]
public class LogoutController:ControllerBase
{
    [HttpPost]
    public IActionResult Logout()
    {
        
        Response.Cookies.Append("jwt", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(-1)
        });

        return Ok(new { message = "Logout efetuado com sucesso" });
    }   
}