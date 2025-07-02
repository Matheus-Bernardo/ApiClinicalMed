using WebApplication1.Core.Entities;
using WebApplication1.Repositories.TypeAppointmentMedicalRepository;

namespace WebApplication1.Services.TypeAppointmentMedicalService;

public class TypeAppointmentService:ITypeAppointmentService
{
    private readonly IAppointmentMedicalRepository _appointmentMedicalRepository;

    public TypeAppointmentService(
        IAppointmentMedicalRepository appointmentMedicalRepository
    )
    {
        _appointmentMedicalRepository = appointmentMedicalRepository;
    }
    
    public async Task<TypeAppointmentMedical> createTypeAppointmentMedical(TypeAppointmentMedical typeAppointmentMedical)
    {
        if(typeAppointmentMedical.description.Length <5 )
            throw new ArgumentException("minimum description length is 5");
        if(typeAppointmentMedical.value <=0)
            throw new ArgumentException("minimum value is 1");
        
        var appointmentCreated = await _appointmentMedicalRepository.createAppointment(typeAppointmentMedical);
        return appointmentCreated;
        
    }

    public async Task<List<TypeAppointmentMedical>> getTypeAppointmentMedical()
    {
        var typesAppointment = await _appointmentMedicalRepository.GetAppointment();
        return typesAppointment.ToList();
        
    }
}