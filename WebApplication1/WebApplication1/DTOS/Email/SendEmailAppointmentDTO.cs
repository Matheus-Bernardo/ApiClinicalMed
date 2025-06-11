namespace WebApplication1.DTOS.Email;

public class SendEmailAppointmentDTO
{
    public string namePatient { get; set; }
    public string emailPatient { get; set; }
    public string nameDoctor { get; set; }
    public string emailDoctor { get; set; }
    public DateTime consultationTime { get; set; }
    public string consultationLink { get; set; }
    public string status { get; set; }
    public string createdAt { get; set; }
    
}