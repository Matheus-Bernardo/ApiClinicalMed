using WebApplication1.Repositories.ConsultationRepository;
using WebApplication1.Repositories.DoctorRepository;
using WebApplication1.Repositories.PatientRepository;
using WebApplication1.Repositories.TypeAppointmentMedicalRepository;
using WebApplication1.Services.ConsultationService;
using WebApplication1.Services.DoctorService;
using WebApplication1.Services.EmailService;
using WebApplication1.Services.MettingService;
using WebApplication1.Services.PatientService;
using WebApplication1.Services.TypeAppointmentMedicalService;
using WebApplication1.Services.Validators.AppointmentMedicalValidator;
using WebApplication1.Services.Validators.ConsultationMedicalValidator;
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
  
        return services;
    }

    public static IServiceCollection AddApplicationTypeConsultationServices(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<IAppointmentMedicalRepository, AppointmentMedicalRepository>();
        
        //services
        services.AddScoped<ITypeAppointmentService, TypeAppointmentService>();
        
        return services;
    }

    public static IServiceCollection AddApplicationValidationServices(this IServiceCollection services)
    {
        services.AddScoped<ICrmValidator, CrmValidator>();
        services.AddScoped<ICpfValidatorService, CpfValidatorService>();
        services.AddScoped<IEmailValidatorService, EmailValidatorService>();
        services.AddScoped<ISusCardValidatorService, SusCardValidatorService>();
        services.AddScoped<IAppointmentMedicalValidatorService, AppointmentMedicalValidatorService>();
        services.AddScoped<IConsultationMedicalValidatorService,ConsultationMedicalValidatorService>();
        
        return services;
    }

    public static IServiceCollection AddApplicationConsultationServices(this IServiceCollection services)
    {
        services.AddScoped<IConsultationService, ConsultationService>();
        services.AddScoped<IConsultationRepository,ConsultationRepository>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IMeetingService, MeetingService>();
        
        return services;
    }
}