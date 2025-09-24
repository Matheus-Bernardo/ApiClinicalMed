using WebApplication1.Enums;

namespace WebApplication1.DTOS.Consultation;

public class FinishConsultationDto
{
    public StatusConsultation status { get; set; } = StatusConsultation.DONE;
    public required int  idMedicalConsultation { get; set;}
    public int prescriptionId { get; set; }
   
}