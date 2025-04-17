namespace WebApplication1.Core.Entities;

public class Doctor:User
{
    public required string crm { get; set; }
    public required string areaSpecialty { get; set; } 
}