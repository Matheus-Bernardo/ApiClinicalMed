using WebApplication1.DTOS.Doctor;

namespace WebApplication1.Services.DoctorService;

public interface IDoctorService
{
    Task<DoctorResponseDto> CreateDoctor(CreateDoctorDto createDoctorDto);
    Task<bool> UpdateDoctor(UpdateDoctorDto updateDoctorDto,int idUserUpdated);
    Task<List<DoctorResponseDto>> GetAllDoctors();
    Task<DoctorResponseDto> GetDoctorById(int id);
    Task<bool> DeleteDoctor(int id);
}