namespace WebApplication1.DTOS.Login;

public class ResponseLoginDto
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}