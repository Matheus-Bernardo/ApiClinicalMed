using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.PrescriptionRepository;

public interface IPrescriptionRepository
{
    Task<Prescription> createPrescription(Prescription prescription);
    Task<Prescription?> GetPrescriptionById(int id);
    
}