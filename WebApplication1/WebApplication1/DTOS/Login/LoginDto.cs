using WebApplication1.Enums;

namespace WebApplication1.DTOS.Login;

public class LoginDto
{
    public required string email { get; set; }
    public required string password { get; set; }
    public required TypeUser typeUser { get; set; }
    
}