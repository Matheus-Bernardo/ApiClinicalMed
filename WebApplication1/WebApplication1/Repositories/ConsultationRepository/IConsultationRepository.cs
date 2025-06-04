using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.ConsultationRepository;

public interface IConsultationRepository
{
    Task<MedicalConsultation?> CreateConsultation(MedicalConsultation medicalConsultation);
    
}