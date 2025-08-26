using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.UserRepository;

public interface IUserRepository
{
    Task<User?> GetUserById(int idUser);
}