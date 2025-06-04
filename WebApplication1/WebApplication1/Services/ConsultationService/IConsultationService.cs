using WebApplication1.DTOS.Consultation;

namespace WebApplication1.Services.ConsultationService;

public interface IConsultationService
{
    Task<CreateConsultationDto> createConsultation(CreateConsultationDto createConsultationDto);
}