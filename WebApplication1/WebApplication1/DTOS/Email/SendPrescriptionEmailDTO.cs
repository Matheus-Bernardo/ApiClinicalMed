namespace WebApplication1.DTOS.Email;

public class SendPrescriptionEmailDTO
{
    public int PrescriptionId { get; set; }         
    public string PatientName { get; set; } = "";   
    public string EmailDoctor { get; set; } = "";
    public string EmailPatient { get; set; } = "";
    public string DoctorName { get; set; } = "";    
    public string CrmDoctor { get; set; } = "";     
    public int ValidityPrescription { get; set; }   
    public List<string> RemedyPrescription { get; set; } = new(); 
    public string DosageRemedy { get; set; } = "";  
    public string FrequencyRemedy { get; set; } = "";
    public string? Observation { get; set; }        
    public DateTime CreatedAt { get; set; }
  
}