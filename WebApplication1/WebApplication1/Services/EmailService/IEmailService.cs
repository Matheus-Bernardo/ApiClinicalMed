using WebApplication1.DTOS.Consultation;

namespace WebApplication1.Services.EmailService;

public interface IEmailService
{
    Task SendAppointmentEmail(ResponseCreateConsultation emailDto);
}