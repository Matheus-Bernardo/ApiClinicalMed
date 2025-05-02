using WebApplication1.DTOS.Doctor;

namespace WebApplication1.Services.DoctorService;

public interface IDoctorService
{
    Task<CreateDoctorDto> CreateDoctor(CreateDoctorDto createDoctorDto);
}