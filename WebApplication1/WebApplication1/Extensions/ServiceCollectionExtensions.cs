using WebApplication1.Repositories.DoctorRepository;
using WebApplication1.Repositories.PatientRepository;
using WebApplication1.Services.DoctorService;
using WebApplication1.Services.PatientService;
using WebApplication1.Services.Validators.CpfValidator;
using WebApplication1.Services.Validators.CrmValidator;
using WebApplication1.Services.Validators.EmailValidator;
using WebApplication1.Services.Validators.SusCardValidator;

namespace WebApplication1.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationPatientServices(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        
        //services
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        
        //validators
        services.AddScoped<ICrmValidator, CrmValidator>();
        services.AddScoped<ICpfValidatorService, CpfValidatorService>();
        services.AddScoped<IEmailValidatorService, EmailValidatorService>();
        services.AddScoped<ISusCardValidatorService, SusCardValidatorService>();
        
        return services;
    }
}