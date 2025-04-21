using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Patient;
using WebApplication1.Enums;
using WebApplication1.Repositories.PatientRepository;
using WebApplication1.Services.Validators.CpfValidator;
using WebApplication1.Services.Validators.EmailValidator;
using WebApplication1.Services.Validators.SusCardValidator;
using WebApplication1.Utils;

namespace WebApplication1.Services.PatientService;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IEmailValidatorService _emailValidatorService;
    private readonly ICpfValidatorService _cpfValidatorService;
    private readonly ISusCardValidatorService _susCardValidatorService;
    private readonly IPasswordHasher _passwordHasher;

    public PatientService(
        IPatientRepository patientRepository,
        IEmailValidatorService emailValidatorService,
        ICpfValidatorService cpfValidatorService,
        ISusCardValidatorService susCardValidatorService,
        IPasswordHasher passwordHasher)
    {
        _patientRepository = patientRepository;
        _emailValidatorService = emailValidatorService;
        _cpfValidatorService = cpfValidatorService;
        _susCardValidatorService = susCardValidatorService;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<CreatePatientDto> CreatePatient(CreatePatientDto patientDto)
    {
        if (await _emailValidatorService.EmailAlreadyRegistered(patientDto.email))
            throw new ArgumentException($"Email {patientDto.email} is already registered");
        
        if (await _cpfValidatorService.CpfAlreadyRegistered(patientDto.cpf))
            throw new ArgumentException($"CPF {patientDto.cpf} is already registered");
        
        if (await _susCardValidatorService.SusCardAlreadyRegistered(patientDto.susCard))
            throw new ArgumentException($"Sus Card {patientDto.susCard} is already registered");
        var passwordHash = _passwordHasher.HashPassword(patientDto.password);
        
        var newPatient = new Patient
        {
            firstName = patientDto.firstName,
            lastName = patientDto.lastName,
            cpf = patientDto.cpf,
            birthDate = patientDto.birthDate,
            phone = patientDto.phone,
            street = patientDto.street,
            district = patientDto.district,
            city = patientDto.city,
            complement = patientDto.complement,
            typeUser = TypeUser.patient,
            email = patientDto.email,
            password = passwordHash,
            susCard = patientDto.susCard,
            messagePhone = patientDto.messagePhone,
            familyHistoryDisease = patientDto.familyHistoryDisease,
            medicalAgreements = patientDto.medicalAgreements,
            createdAt = DateTime.UtcNow,
            updatedAt = null
        };
        
        await _patientRepository.AddPatient(newPatient);
        return patientDto;

    }
}