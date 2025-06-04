using WebApplication1.Enums;

namespace WebApplication1.DTOS.Consultation;

public class CreateConsultationDto
{
    public required int typeAppointmentMedical {get; set;}
    public required int doctorId {get; set;}
    public required int patientId {get; set;}
    public required DateTime consultationTime {get; set;}
    public required StatusConsultation status { get; set; }
    public required string consultationLink { get; set; }
    public double agreementDiscount { get; set; } 
    
}