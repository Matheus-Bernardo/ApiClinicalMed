namespace WebApplication1.Services.Validators.CpfValidator;

public interface ICpfValidatorService
{
    Task<bool> CpfAlreadyRegistered(string cpf);
}