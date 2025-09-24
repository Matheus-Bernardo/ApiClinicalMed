using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Consultation;
using WebApplication1.DTOS.Email;
using WebApplication1.Enums;
using WebApplication1.Repositories.ConsultationRepository;
using WebApplication1.Services.EmailService;
using WebApplication1.Services.MettingService;
using WebApplication1.Services.PrescriptionService;
using WebApplication1.Services.Validators.AppointmentMedicalValidator;
using WebApplication1.Services.Validators.ConsultationMedicalValidator;
using WebApplication1.Utils;

namespace WebApplication1.Services.ConsultationService;

public class ConsultationService: IConsultationService
{
    private readonly FindUser _findUser;
    private readonly IEmailService _emailService;
    private readonly IMeetingService _meetingService;
    private readonly IPrescriptionService _prescriptionService;
    private readonly IConsultationRepository _consultationRepository;
    private readonly IAppointmentMedicalValidatorService _appointmentMedicalValidatorService;
    private readonly IConsultationMedicalValidatorService _consultationMedicalValidatorService;

    public ConsultationService(
        FindUser findUser,
        IEmailService emailService,
        IMeetingService meetingService,
        IPrescriptionService prescriptionService,
        IConsultationRepository consultationRepository,
        IAppointmentMedicalValidatorService appointmentMedicalValidatorService,
        IConsultationMedicalValidatorService consultationMedicalValidatorService)
    {
        _findUser = findUser;
        _emailService = emailService;
        _meetingService = meetingService;
        _prescriptionService = prescriptionService;
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
            consultationLink = await _meetingService.GenerateMeetingLink(new ResponseCreateConsultation
            {
                consultationTime = createConsultationDto.consultationTime,
                nameDoctor = doctorFind.firstName + " " + doctorFind.lastName,
                namePatient = patientFind.firstName + " " + patientFind.lastName,
                emailDoctor = doctorFind.email,
                emailPatient = patientFind.email
            }),
            status = StatusConsultation.PENDING,
            justificationUpdate = "valor inicial",
            consultationTime = createConsultationDto.consultationTime,
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

    public async Task<List<MedicalConsultation>> GetMedicalConsultations()
    {
        return  await _consultationRepository.GetConsults();
    }

    public async Task<List<ResponseConsultByUser>> GetMedicalConsultationsByUserId(int userId)
    {
        var consults = await _consultationRepository.GetMedicalConsultationsByUserId(userId);
        
        return consults.Select(c => new ResponseConsultByUser
        {
            ConsultationId = c.Id,
            typeAppointment = c.typeAppointmentMedical.ToString(),
            consultationTime = c.consultationTime,
            consultationLink = c.consultationLink,
            status = c.status.ToString(),
            doctorName = c.Doctor.firstName + " " + c.Doctor.lastName,
            patientName = c.Patient.firstName + " " + c.Patient.lastName,
            crmDoctor = c.Doctor.crm,
        }).ToList();
       
    }

    public async Task<MedicalConsultation> finishMedicalConsultationWithPrescription(FinishConsultationDto finishConsultationDto)
    {
        var findConsult = await _consultationRepository.GetConsultById(finishConsultationDto.idMedicalConsultation); 
        if ( findConsult==null)
        {
            throw new ArgumentException("Medical consultation not found");
        }

        try
        {
            findConsult.updatedAt = DateTime.UtcNow;
            findConsult.status = finishConsultationDto.status;
            findConsult.idPrescription = finishConsultationDto.prescriptionId;
            await _consultationRepository.finishConsultationUpdate(findConsult);

            var findPrescription = await _prescriptionService.getPrescriptionById(finishConsultationDto.prescriptionId);
            
            
            var emailDto = new SendPrescriptionEmailDTO
            {
                PrescriptionId = finishConsultationDto.prescriptionId,
                PatientName = findPrescription.patientName,
                DoctorName = findPrescription.doctorName,
                CrmDoctor = findPrescription.crmDoctor,
                EmailDoctor = findConsult.Doctor.email,
                EmailPatient = findConsult.Patient.email,
                ValidityPrescription = findPrescription.validityPrescription,
                RemedyPrescription = new List<string>(findPrescription.remedyPrescription),
                DosageRemedy = findPrescription.dosageRemedy,
                FrequencyRemedy = findPrescription.frequencyRemedy,
                Observation = findPrescription.observation ?? "Sem observações",
                CreatedAt = DateTime.UtcNow
            };
            await _emailService.SendPrescriptionEmail(finishConsultationDto.prescriptionId, emailDto);
            

        }
        catch (Exception e)
        {
            throw;}
        
        
        
        return findConsult;

    }

    public async Task<MedicalConsultation> finishMedicalConsultationWithoutPrescription(FinishConsultationDto finishConsultationDto)
    {
        var findConsult = await _consultationRepository.GetConsultById(finishConsultationDto.idMedicalConsultation); 
        if ( findConsult==null)
        {
            throw new ArgumentException("Medical consultation not found");
        }

        try{
            findConsult.idPrescription = null;
            findConsult.updatedAt = DateTime.UtcNow;
            findConsult.status = finishConsultationDto.status;
            await _consultationRepository.finishConsultationUpdate(findConsult);
            
        }catch (Exception e){throw new ArgumentException("update failure");}
        
        return findConsult;
    }
}