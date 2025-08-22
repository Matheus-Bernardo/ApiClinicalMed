namespace WebApplication1.Core.Entities;

public class Prescription
{
    public int id { get; set; }
    public required string patientName { get; set; }
    public required string doctorName { get; set; }
    public required int validityPrescription{get;set;}
    public required string crmDoctor{get;set;}
    public required List<string>  remedyPrescription{get;set;}
    public string frequency{get;set;}
    public string dosageRemedy{get;set;}
    public string frequencyRemedy{get;set;}
    public string observation{get;set;}
    public DateTime createdAt { get; set; } = DateTime.UtcNow;  
    
}