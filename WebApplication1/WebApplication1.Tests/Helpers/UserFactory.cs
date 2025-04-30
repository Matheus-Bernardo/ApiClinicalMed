using WebApplication1.Core.Entities;
using WebApplication1.Enums;

namespace WebApplication1.Tests.Helpers;

public class UserFactory
{
    public static User CreateValidPatient(string email, string password, TypeUser typeUser)
    {
        return new Patient
        {
            Id = 1,
            firstName = "Test",
            lastName = "User",
            cpf = "12345678900",
            birthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
            phone = "11999999999",
            street = "Rua Teste",
            district = "Centro",
            city = "Cidade",
            complement = "Apto 101",
            typeUser = typeUser,
            email = email,
            password = password,
            createdAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow,
            susCard = "987654321",
            messagePhone = "Emergência",
            familyHistoryDisease = new List<string> { "Diabetes" },
            medicalAgreements = new List<string> { "Unimed" }
        };
        
        
    }

    public static Doctor CreateValidDoctor(string email, string password, TypeUser typeUser)
    {
        return new Doctor
        {
            Id = 2,
            firstName = "Doc",
            lastName = "Tor",
            cpf = "11122233344",
            birthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-40)),
            phone = "11888888888",
            street = "Av. Saúde",
            district = "Hospitalar",
            city = "Curecity",
            complement = "Bloco B",
            typeUser = typeUser,
            email = email,
            password = password,
            createdAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow,
            crm = "CRM123456",
            areaSpecialty = "Clínico Geral"
        };
    }
}