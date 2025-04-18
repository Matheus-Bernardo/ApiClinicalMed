using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Login;
using WebApplication1.Utils;

namespace WebApplication1.Services.LoginService;

public class LoginService: ILoginService
{
    private readonly FindUser _findUser;
    private  PasswordHasher _passwordHasher;

    public LoginService(FindUser findUser, PasswordHasher passwordHasher)
    {
        _findUser = findUser;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<User?> login(LoginDto loginDto)
    {
        var user = await _findUser.FindUserByEmail(loginDto.email);
        
        if (user == null)
            throw new ArgumentException($"The Email {loginDto.email} not found");
        
        if (await _findUser.FindUserByUserType(loginDto.email,loginDto.typeUser)==null)
            throw new ArgumentException($"You are not a {loginDto.typeUser.ToString()} user");

        if (!_passwordHasher.VerifyHashedPassword(user.password,loginDto.password))
            throw new ArgumentException("Invalid password");
        
        return user;
        
    }
}