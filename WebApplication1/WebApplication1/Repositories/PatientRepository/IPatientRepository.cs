using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.PatientRepository;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllPatients();
    Task<Patient?> GetPatientById(int id);
    Task<bool> DeletePatient(Patient patient);
    Task<Patient?> AddPatient(Patient patient);
    Task<Patient?> UpdatePatient(Patient patient);

}