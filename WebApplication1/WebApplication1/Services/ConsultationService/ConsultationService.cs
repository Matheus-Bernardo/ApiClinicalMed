using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Consultation;
using WebApplication1.Enums;
using WebApplication1.Repositories.ConsultationRepository;
using WebApplication1.Services.Validators.AppointmentMedicalValidator;
using WebApplication1.Services.Validators.ConsultationMedicalValidator;
using WebApplication1.Utils;

namespace WebApplication1.Services.ConsultationService;

public class ConsultationService: IConsultationService
{
    private readonly FindUser _findUser;
    private readonly IConsultationRepository _consultationRepository;
    private readonly IAppointmentMedicalValidatorService _appointmentMedicalValidatorService;
    private readonly IConsultationMedicalValidatorService _consultationMedicalValidatorService;

    public ConsultationService(
        FindUser findUser,
        IConsultationRepository consultationRepository,
        IAppointmentMedicalValidatorService appointmentMedicalValidatorService,
        IConsultationMedicalValidatorService consultationMedicalValidatorService)
    {
        _findUser = findUser;
        _consultationRepository = consultationRepository;
        _appointmentMedicalValidatorService = appointmentMedicalValidatorService;
        _consultationMedicalValidatorService = consultationMedicalValidatorService;
    }
    
    public async Task<ResponseCreateConsultation> createConsultation(CreateConsultationDto createConsultationDto)
    {
        if (await _findUser.FindPatientById(createConsultationDto.patientId) == null)
            throw new ArgumentException("Patient not found");
        
        if(await _findUser.FindDoctorById(createConsultationDto.doctorId) == null)
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
            doctorId = consultationCreated.doctorId,
            patientId = consultationCreated.patientId,
            consultationLink = consultationCreated.consultationLink
        };
        
        
        return response;
    }
}