using WebApplication1.Enums;

namespace WebApplication1.Core.Entities;

public class MedicalConsultation
{
    public int Id { get; set; }
    public required int typeAppointmentMedical {get; set;}
    public required int doctorId {get; set;}
    public required int patientId {get; set;}
    public required DateTime consultationTime {get; set;}
    public required StatusConsultation status { get; set; }
    public required string consultationLink { get; set; }
    public double agreementDiscount { get; set; } 
    public string justificationUpdate { get; set; } = null;
    public required DateTime createdAt { get; set; } = DateTime.Now;
    public required DateTime? updatedAt { get; set; } = null;
    
    
    public Doctor Doctor { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
    public TypeAppointmentMedical AppointmentType { get; set; } = null!;
    
}