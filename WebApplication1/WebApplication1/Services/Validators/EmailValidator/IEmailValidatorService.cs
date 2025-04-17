namespace WebApplication1.Services.Validators.EmailValidator;

public interface IEmailValidatorService
{
    Task<bool> EmailAlreadyRegistered(string email);
}