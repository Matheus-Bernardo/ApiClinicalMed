using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.DoctorRepository;

public interface IDoctorRepository
{
    Task<Doctor?> CreateDoctor(Doctor doctor);
    Task<bool> UpdateDoctor(Doctor doctor);
    Task<List<Doctor>> getAllDoctors();
    Task<Doctor?> GetDoctorById(int id);
    Task<bool> DeleteDoctor(Doctor doctor);
}