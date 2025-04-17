namespace WebApplication1.Services.Validators.SusCardValidator;

public interface ISusCardValidatorService
{
    Task<bool> SusCardAlreadyRegistered(string Suscard);
}