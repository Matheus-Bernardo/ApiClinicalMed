using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Repositories.DoctorRepository;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Doctor?> CreateDoctor(Doctor doctor)
    {
        await _context.Doctor.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }
}