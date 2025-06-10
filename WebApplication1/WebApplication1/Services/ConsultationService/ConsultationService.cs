using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Consultation;
using WebApplication1.Enums;
using WebApplication1.Repositories.ConsultationRepository;
using WebApplication1.Services.EmailService;
using WebApplication1.Services.Validators.AppointmentMedicalValidator;
using WebApplication1.Services.Validators.ConsultationMedicalValidator;
using WebApplication1.Utils;

namespace WebApplication1.Services.ConsultationService;

public class ConsultationService: IConsultationService
{
    private readonly FindUser _findUser;
    private readonly IEmailService _emailService;
    private readonly IConsultationRepository _consultationRepository;
    private readonly IAppointmentMedicalValidatorService _appointmentMedicalValidatorService;
    private readonly IConsultationMedicalValidatorService _consultationMedicalValidatorService;

    public ConsultationService(
        FindUser findUser,
        IEmailService emailService,
        IConsultationRepository consultationRepository,
        IAppointmentMedicalValidatorService appointmentMedicalValidatorService,
        IConsultationMedicalValidatorService consultationMedicalValidatorService)
    {
        _findUser = findUser;
        _emailService = emailService;
        _consultationRepository = consultationRepository;
        _appointmentMedicalValidatorService = appointmentMedicalValidatorService;
        _consultationMedicalValidatorService = consultationMedicalValidatorService;
    }
    
    public async Task<ResponseCreateConsultation> createConsultation(CreateConsultationDto createConsultationDto)
    {
        var patientFind = await  _findUser.FindPatientById(createConsultationDto.patientId);
        var doctorFind = await _findUser.FindDoctorById(createConsultationDto.doctorId);
        
        if (patientFind == null)
            throw new ArgumentException("Patient not found");
        
        if(doctorFind == null)
            throw new ArgumentException("Doctor not found");
        
        if(!await _appointmentMedicalValidatorService.AppointmentMedicalExists(createConsultationDto.typeAppointmentMedical))
            throw new ArgumentException("Appointment medical not found");
        if (createConsultationDto.consultationTime < DateTime.UtcNow)
            throw new ArgumentException("Date and time cannot be in the future");
        
        if(await _consultationMedicalValidatorService.IsConsultationHourAvailable(createConsultationDto.doctorId,createConsultationDto.consultationTime) )
            throw new ArgumentException("Consultation hour not available");

        var newConsultation = new MedicalConsultation
        {
            typeAppointmentMedical = 1,
            doctorId = createConsultationDto.doctorId,
            patientId = createConsultationDto.patientId,
            consultationTime = createConsultationDto.consultationTime,
            status = StatusConsultation.PENDING,
            consultationLink = "teste por enquanto",
            justificationUpdate = "valor inicial",
            createdAt = DateTime.UtcNow,
            updatedAt = null,
        };
        
        var consultationCreated = await _consultationRepository.CreateConsultation(newConsultation);

        var response = new ResponseCreateConsultation
        {
            consultationTime = consultationCreated.consultationTime,
            nameDoctor = doctorFind.firstName + " " + doctorFind.lastName,
            namePatient = patientFind.firstName + " " + patientFind.lastName,
            consultationLink = consultationCreated.consultationLink,
            status = newConsultation.status,
            createdAt = consultationCreated.createdAt,
            emailDoctor = doctorFind.email,
            emailPatient = patientFind.email
        };
        
        await _emailService.SendAppointmentEmail(response);
        
        return response;
    }
}