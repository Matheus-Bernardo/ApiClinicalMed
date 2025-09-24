using WebApplication1.Core.Entities;

namespace WebApplication1.Services.PrescriptionService;

public interface IPrescriptionService
{
    Task<Prescription> createPrescription(Prescription prescription);
    Task<Prescription> getPrescriptionById(int id);
}