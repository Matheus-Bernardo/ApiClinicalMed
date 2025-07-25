using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.ConsultationRepository;

public interface IConsultationRepository
{
    Task<MedicalConsultation?> CreateConsultation(MedicalConsultation medicalConsultation);
    Task<List<MedicalConsultation>> GetConsults();
    Task<List<MedicalConsultation>> GetMedicalConsultationsByUserId(int userId);

}