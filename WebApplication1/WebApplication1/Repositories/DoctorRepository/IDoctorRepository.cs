using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.DoctorRepository;

public interface IDoctorRepository
{
    Task<Doctor?> CreateDoctor(Doctor doctor);
}