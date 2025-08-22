using WebApplication1.Core.Entities;
using WebApplication1.Repositories.PrescriptionRepository;
using WebApplication1.Services.Validators.CrmValidator;
using WebApplication1.Utils;

namespace WebApplication1.Services.PrescriptionService;

public class PrescriptionService:IPrescriptionService
{
    private readonly ICrmValidator _crmValidator;
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(ICrmValidator crmValidator, IPrescriptionRepository prescriptionRepository)
    {
        _crmValidator = crmValidator;
        _prescriptionRepository = prescriptionRepository;
    }
    
    
    public async Task<Prescription> createPrescription(Prescription prescription)
    {
        if (!await _crmValidator.CrmAlreadyRegistered(prescription.crmDoctor))
            throw new ArgumentException("Crm Doctor not found");
        
        var prescriptionCreated = await _prescriptionRepository.createPrescription(prescription);
        return prescriptionCreated;

    }

    public Task<Prescription> getPrescriptionById(int id)
    {
        throw new NotImplementedException();
    }
}