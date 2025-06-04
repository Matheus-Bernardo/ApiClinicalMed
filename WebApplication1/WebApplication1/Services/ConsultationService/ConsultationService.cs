using WebApplication1.DTOS.Consultation;
using WebApplication1.Utils;

namespace WebApplication1.Services.ConsultationService;

public class ConsultationService: IConsultationService
{
    private readonly FindUser _findUser;

    public ConsultationService(FindUser findUser)
    {
        _findUser = findUser;
    }
    
    public async Task<CreateConsultationDto> createConsultation(CreateConsultationDto createConsultationDto)
    {
        if (await _findUser.FindPatientById(createConsultationDto.patientId) == null)
            throw new ArgumentException("Patient not found");
        
        if(await _findUser.FindDoctorById(createConsultationDto.doctorId) == null)
            throw new ArgumentException("Doctor not found");
        //ainda preciso validar que o dia e a hora estejam disponiveis e o id do tipo da consulta
        return createConsultationDto;
    }
}