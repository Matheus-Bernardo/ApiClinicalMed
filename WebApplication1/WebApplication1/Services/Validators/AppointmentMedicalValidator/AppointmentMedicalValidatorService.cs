using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Services.Validators.AppointmentMedicalValidator;

public class AppointmentMedicalValidatorService: IAppointmentMedicalValidatorService
{
    private readonly AppDbContext _dbContext;

    public AppointmentMedicalValidatorService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> AppointmentMedicalExists(int id)
    {
       return await _dbContext.TypeAppointmentMedical.AnyAsync(appointment => appointment.id == id);
    }
}