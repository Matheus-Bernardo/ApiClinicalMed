namespace WebApplication1.Services.Validators.CrmValidator;

public interface ICrmValidator
{
    Task<bool> CrmAlreadyRegistered(string crm);
}