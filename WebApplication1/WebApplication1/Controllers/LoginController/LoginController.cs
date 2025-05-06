using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOS.Login;
using WebApplication1.Services.LoginService;

namespace WebApplication1.Controllers.LoginController;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var userLogin = await _loginService.login(loginDto);
        return Ok(userLogin);
    }
}