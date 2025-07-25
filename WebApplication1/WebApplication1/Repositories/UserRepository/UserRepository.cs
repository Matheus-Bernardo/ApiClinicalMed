using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Repositories.UserRepository;

public class UserRepository:IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserById(int idUser)
    {
        return await _context.User.FindAsync(idUser);
    }
}