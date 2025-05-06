namespace WebApplication1.DTOS.Patient;

public class PatientResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string SusCard { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string City { get; set; } = default!;
}