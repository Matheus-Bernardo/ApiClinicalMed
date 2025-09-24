using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Consultation;

namespace WebApplication1.Repositories.ConsultationRepository;

public interface IConsultationRepository
{
    Task<MedicalConsultation?> CreateConsultation(MedicalConsultation medicalConsultation);
    Task<List<MedicalConsultation>> GetConsults();
    Task<MedicalConsultation?> GetConsultById(int consultId);
    Task<List<MedicalConsultation>> GetMedicalConsultationsByUserId(int userId);
    Task <bool> finishConsultationUpdate(MedicalConsultation medicalConsultation);

}