using WebApplication1.Enums;

namespace WebApplication1.DTOS.Patient;

public class CreatePatientDto
{
    public required string firstName { get; set; }
    public required string lastName { get; set; }
    public required string cpf { get; set; }
    public required DateOnly birthDate { get; set; }
    public required string phone { get; set; }
    public required string street { get; set; }
    public required string district { get; set; }
    public required string city { get; set; }
    public required string complement { get; set; }
    public required string email { get; set; }
    public required string password { get; set; }
    public required string susCard { get; set; } //Card Brazilian Unified Health System
    public required string messagePhone { get; set; }
    public required List<string> familyHistoryDisease { get; set; }
    public required List<string> medicalAgreements { get; set; }
}