using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Consultation;

namespace WebApplication1.Services.ConsultationService;

public interface IConsultationService
{
    Task<ResponseCreateConsultation> createConsultation(CreateConsultationDto createConsultationDto);
    Task<List<MedicalConsultation>> GetMedicalConsultations();
    Task<List<MedicalConsultation>> GetMedicalConsultationsByUserId(int userId);
}