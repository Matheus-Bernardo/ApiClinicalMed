namespace WebApplication1.DTOS.Consultation;

public class ResponseConsultByUser
{
    public string typeAppointment { get; set; }
    public string doctorName { get; set; }
    public string patientName { get; set; }
    public DateTime consultationTime { get; set; }
    public string consultationLink { get; set; }
    public string status { get; set; }
}
