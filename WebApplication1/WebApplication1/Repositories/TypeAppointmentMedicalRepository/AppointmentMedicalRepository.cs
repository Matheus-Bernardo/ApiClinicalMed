using WebApplication1.Core.Entities;
using WebApplication1.Infrastructure.Data;

namespace WebApplication1.Repositories.TypeAppointmentMedicalRepository;

public class AppointmentMedicalRepository:IAppointmentMedicalRepository
{
    private readonly AppDbContext _context;

    public AppointmentMedicalRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<TypeAppointmentMedical?> createAppointment(TypeAppointmentMedical typeAppointmentMedical)
    {
        await _context.TypeAppointmentMedical.AddAsync(typeAppointmentMedical);
        await _context.SaveChangesAsync();
        return typeAppointmentMedical;
    }
}