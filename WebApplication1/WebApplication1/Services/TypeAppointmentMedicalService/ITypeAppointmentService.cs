using WebApplication1.Core.Entities;

namespace WebApplication1.Services.TypeAppointmentMedicalService;

public interface ITypeAppointmentService
{
    Task<TypeAppointmentMedical> createTypeAppointmentMedical(TypeAppointmentMedical typeAppointmentMedical);
    
}