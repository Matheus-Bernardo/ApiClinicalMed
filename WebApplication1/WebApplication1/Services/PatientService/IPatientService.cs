using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Patient;

namespace WebApplication1.Services.PatientService;

public interface IPatientService
{
    Task<List<PatientResponseDto>> GetAllPatient();
    Task<PatientResponseDto?> GetPatientById(int id);
    Task<bool> DeletePatient(int id);
    Task<PatientResponseDto> CreatePatient(CreatePatientDto patient);
    
    Task<UpdatePatientDto> UpdatePatient(UpdatePatientDto patient, int id);
    
    
    
}