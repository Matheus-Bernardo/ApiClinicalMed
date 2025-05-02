using WebApplication1.DTOS.Patient;

namespace WebApplication1.Services.PatientService;

public interface IPatientService
{
    Task<CreatePatientDto> CreatePatient(CreatePatientDto patient);
    
    Task<UpdatePatientDto> UpdatePatient(UpdatePatientDto patient, int id);
    
    
}