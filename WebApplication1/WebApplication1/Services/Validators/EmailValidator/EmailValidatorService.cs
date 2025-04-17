using Microsoft.EntityFrameworkCore;
using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Services.Validators.EmailValidator;

public class EmailValidatorService:IEmailValidatorService
{
    private readonly AppDbContext _dbContext;

    public EmailValidatorService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> EmailAlreadyRegistered(string email)
    {
        return await _dbContext.User.AnyAsync(user => user.email == email);
    }
}