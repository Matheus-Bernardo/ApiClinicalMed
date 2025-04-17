using WebApplication1.Enums;
namespace WebApplication1.Core.Entities;
public abstract class User
{
    public int Id { get; set; }
    public required string firstName { get; set; }
    public required string lastName { get; set; }
    public required string cpf { get; set; }
    public required DateOnly birthDate { get; set; }
    public required string phone { get; set; }
    public required string street { get; set; }
    public required string district { get; set; }
    public required string city { get; set; }
    public required string complement { get; set; }
    public required TypeUser typeUser { get; set; }
    public required string email { get; set; }
    public required string password { get; set; }
    public required DateTime createdAt { get; set; } = DateTime.Now;
    public required DateTime? updatedAt { get; set; } = null;
    
}