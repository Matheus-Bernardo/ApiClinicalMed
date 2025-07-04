using Microsoft.EntityFrameworkCore;
using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Repositories.ConsultationRepository;

public class ConsultationRepository: IConsultationRepository
{
    private readonly AppDbContext _context;

    public ConsultationRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<MedicalConsultation?> CreateConsultation(MedicalConsultation medicalConsultation)
    {
        await _context.MedicalConsultation.AddAsync(medicalConsultation);
        await _context.SaveChangesAsync();
        return medicalConsultation;
    }

    public async Task<List<MedicalConsultation>> GetConsults()
    {
        return await _context.MedicalConsultation.ToListAsync();
    }
}