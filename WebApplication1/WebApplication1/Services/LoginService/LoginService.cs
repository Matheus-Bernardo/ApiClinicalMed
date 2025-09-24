using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Login;
using WebApplication1.Enums;
using WebApplication1.Utils;

namespace WebApplication1.Services.LoginService;

public class LoginService: ILoginService
{
    private readonly FindUser _findUser;
    private readonly IJwtService _jwtService;
    private  readonly IPasswordHasher _passwordHasher;

    public LoginService(FindUser findUser, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _findUser = findUser;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }
    
    public async Task<ResponseLoginDto> login(LoginDto loginDto)
    {
        var user = await _findUser.FindUserByEmail(loginDto.email);
        
        if (user == null)
            throw new ArgumentException($"The Email {loginDto.email} not found");
        
        if (await _findUser.FindUserByUserType(loginDto.email,loginDto.typeUser)==null)
            throw new ArgumentException($"You are not a {loginDto.typeUser.ToString()} user");

        if (!_passwordHasher.VerifyHashedPassword(user.password,loginDto.password))
            throw new ArgumentException("Invalid password");
        
        var token = _jwtService.GenerateToken(user);

       return new ResponseLoginDto()
        {
            Id = user.Id,
            Token = token,
            FirstName = user.firstName,
            Role = user.typeUser.ToString()
        };
        
    }
}