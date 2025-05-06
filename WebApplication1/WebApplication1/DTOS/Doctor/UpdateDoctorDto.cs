namespace WebApplication1.DTOS.Doctor;

public class UpdateDoctorDto
{
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? cpf { get; set; }
    public DateOnly? birthDate { get; set; }
    public string? phone { get; set; }
    public string? street { get; set; }
    public string? district { get; set; }
    public string? city { get; set; }
    public string? complement { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public string? crm { get; set; }
    public string? areaSpecialty { get; set; } 
}