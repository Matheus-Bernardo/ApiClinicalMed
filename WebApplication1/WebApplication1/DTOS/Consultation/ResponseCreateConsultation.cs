using WebApplication1.Enums;

namespace WebApplication1.DTOS.Consultation;

public class ResponseCreateConsultation
{
    public string emailPatient { get; set; }
    public required string namePatient {get; set;}
    
    public string emailDoctor { get; set; }
    public required string nameDoctor {get; set;}
    public required DateTime consultationTime {get; set;}
    public string? consultationLink { get; set; }
    public StatusConsultation status { get; set; }
    public DateTime createdAt { get; set; }
    
}