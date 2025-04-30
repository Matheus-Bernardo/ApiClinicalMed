using WebApplication1.Core.Entities;

namespace WebApplication1.Utils;

public interface IJwtService
{
    string GenerateToken(User user);
}