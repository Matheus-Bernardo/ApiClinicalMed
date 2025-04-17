using WebApplication1.Repositories.PatientRepository;
using WebApplication1.Services.PatientService;
using WebApplication1.Services.Validators.CpfValidator;
using WebApplication1.Services.Validators.EmailValidator;
using WebApplication1.Services.Validators.SusCardValidator;

namespace WebApplication1.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationPatientServices(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<IPatientRepository, PatientRepository>();
        
        //services
        services.AddScoped<IPatientService, PatientService>();
        
        //validators
        services.AddScoped<IEmailValidatorService, EmailValidatorService>();
        services.AddScoped<ICpfValidatorService, CpfValidatorService>();
        services.AddScoped<ISusCardValidatorService, SusCardValidatorService>();
        
        return services;
    }
}