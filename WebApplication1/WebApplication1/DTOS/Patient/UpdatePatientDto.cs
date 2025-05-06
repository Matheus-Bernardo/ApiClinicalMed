namespace WebApplication1.DTOS.Patient;

public class UpdatePatientDto
{
    public  string? firstName { get; set; }
    public  string? lastName { get; set; }
    public  string? cpf { get; set; }
    public  DateOnly? birthDate { get; set; }
    public  string? phone { get; set; }
    public  string? street { get; set; }
    public  string? district { get; set; }
    public  string? city { get; set; }
    public  string? complement { get; set; }
    public  string? email { get; set; }
    public  string? password { get; set; }
    public  string? susCard { get; set; } //Card Brazilian Unified Health System
    public  string? messagePhone { get; set; }
    public  List<string>? familyHistoryDisease { get; set; }
    public  List<string>? medicalAgreements { get; set; }
}