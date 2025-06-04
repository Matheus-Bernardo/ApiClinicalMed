using Microsoft.EntityFrameworkCore;
using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Utils;

namespace WebApplication1.Repositories.DoctorRepository;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;
    
    public DoctorRepository(AppDbContext context, FindUser findUser)
    {
        _context = context;
        
    }
    
    public async Task<Doctor?> CreateDoctor(Doctor doctor)
    {
        await _context.Doctor.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<bool> UpdateDoctor(Doctor doctor)
    {
        _context.Doctor.Update(doctor);
        var result = await _context.SaveChangesAsync();
        return true;

    }

    public async Task<List<Doctor>> getAllDoctors()
    {
        return await _context.Doctor.ToListAsync();
    }

    public async Task<Doctor?> GetDoctorById(int id)
    {
        return await _context.Doctor.FindAsync(id);
    }

    public async Task<bool> DeleteDoctor(Doctor doctor)
    {
        _context.Doctor.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }
}