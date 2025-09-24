using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Repositories.PrescriptionRepository;

public class PrescriptionRepository:IPrescriptionRepository
{
    private readonly AppDbContext _context;
    
    public PrescriptionRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Prescription> createPrescription(Prescription prescription)
    { 
        await _context.Prescription.AddAsync(prescription);
        await _context.SaveChangesAsync();
        return prescription;
    }

    public async Task<Prescription?> GetPrescriptionById(int id)
    {
        return await _context.Prescription.FindAsync(id);
    }
}