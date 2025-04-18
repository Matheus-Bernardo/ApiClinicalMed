using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Login;

namespace WebApplication1.Services.LoginService;

public interface ILoginService
{
    public Task<User?> login(LoginDto loginDto); 
}