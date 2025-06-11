namespace WebApplication1.Services.Validators.AppointmentMedicalValidator;

public interface IAppointmentMedicalValidatorService
{
    Task<bool> AppointmentMedicalExists(int id);
    
}