namespace WebApplication1.Core.Entities;

public class Patient : User
{
    public required string susCard { get; set; } //Card Brazilian Unified Health System
    public required string messagePhone { get; set; }
    public required List<string> familyHistoryDisease { get; set; }
    public required List<string> medicalAgreements { get; set; }
    
}