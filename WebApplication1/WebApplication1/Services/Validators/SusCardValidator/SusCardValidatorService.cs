using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Services.Validators.SusCardValidator;

public class SusCardValidatorService : ISusCardValidatorService
{
    private readonly AppDbContext _dbContext;

    public SusCardValidatorService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> SusCardAlreadyRegistered(string Suscard)
    {
        return await _dbContext.Patient.AnyAsync(patient => patient.susCard == Suscard);
    }
}