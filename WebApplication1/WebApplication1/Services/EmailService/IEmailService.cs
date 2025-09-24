using WebApplication1.DTOS.Consultation;
using WebApplication1.DTOS.Email;

namespace WebApplication1.Services.EmailService;

public interface IEmailService
{
    Task SendAppointmentEmail(ResponseCreateConsultation emailDto);
    Task SendPrescriptionEmail(int idPrescription,SendPrescriptionEmailDTO emailDto);
}