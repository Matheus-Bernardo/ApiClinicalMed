using Microsoft.EntityFrameworkCore;
using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Repositories.PatientRepository;

public class PatientRepository: IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Patient?> AddPatient(Patient patient)
    {
        await _context.Patient.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient?> UpdatePatient(Patient patient)
    {
         _context.Patient.Update(patient);
         await _context.SaveChangesAsync();
         return patient;
    }
}