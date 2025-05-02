using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Services.Validators.CrmValidator;

public class CrmValidator : ICrmValidator
{
    private readonly AppDbContext _dbContext;

    public CrmValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CrmAlreadyRegistered(string crm)
    {
        return await _dbContext.Doctor.AnyAsync(doctor => doctor.crm == crm); 
    }
}