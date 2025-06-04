using WebApplication1.Core.Entities;

namespace WebApplication1.Repositories.TypeAppointmentMedicalRepository;

public interface IAppointmentMedicalRepository
{
    Task<TypeAppointmentMedical?> createAppointment(TypeAppointmentMedical typeAppointmentMedical);
}