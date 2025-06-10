using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Services.Validators.ConsultationMedicalValidator;

public class ConsultationMedicalValidatorService:IConsultationMedicalValidatorService
{
    private readonly AppDbContext _dbContext;

    public ConsultationMedicalValidatorService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> IsConsultationHourAvailable(int idDoctor, DateTime suggestedDateTime)
    {
         var deubom = await _dbContext.MedicalConsultation
            .AnyAsync(medicalConsultation =>
                medicalConsultation.doctorId == idDoctor &&
                medicalConsultation.consultationTime == suggestedDateTime);
         return deubom;
    }

}