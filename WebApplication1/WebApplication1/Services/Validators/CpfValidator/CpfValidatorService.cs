using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Services.Validators.CpfValidator;

public class CpfValidatorService: ICpfValidatorService
{
    private readonly AppDbContext _dbContext;

    public CpfValidatorService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CpfAlreadyRegistered(string cpf)
    {
        return await _dbContext.User.AnyAsync(user => user.cpf == cpf); 
    }
}