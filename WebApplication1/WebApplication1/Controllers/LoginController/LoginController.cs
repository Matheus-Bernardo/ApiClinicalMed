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
        try
        {
            var userLogin = await _loginService.login(loginDto);
            return Ok(userLogin);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500,"Erro Interno "+ e.Message);
        }
    }
}