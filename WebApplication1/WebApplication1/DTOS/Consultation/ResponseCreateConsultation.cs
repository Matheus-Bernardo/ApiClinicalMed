namespace WebApplication1.DTOS.Consultation;

public class ResponseCreateConsultation
{
    public required int doctorId {get; set;}
    public required int patientId {get; set;}
    public required DateTime consultationTime {get; set;}
    public string? consultationLink { get; set; }
    
}