namespace WebApplication1.Services.Validators.ConsultationMedicalValidator;

public interface IConsultationMedicalValidatorService
{
    Task<bool> IsConsultationHourAvailable(int idDoctor, DateTime suggestedDateTime);
}