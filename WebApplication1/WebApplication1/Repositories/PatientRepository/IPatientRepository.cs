using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.PatientRepository;

public interface IPatientRepository
{
    Task<Patient?> AddPatient(Patient patient);
}