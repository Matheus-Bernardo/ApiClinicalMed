using WebApplication1.Core.Entities;
using WebApplication1.DTOS.Doctor;
using WebApplication1.Enums;
using WebApplication1.Repositories.DoctorRepository;
using WebApplication1.Services.Validators.CpfValidator;
using WebApplication1.Services.Validators.CrmValidator;
using WebApplication1.Services.Validators.EmailValidator;
using WebApplication1.Utils;

namespace WebApplication1.Services.DoctorService;

public class DoctorService: IDoctorService
{
    
    private readonly ICrmValidator _crmValidator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ICpfValidatorService _cpfValidatorService;
    private readonly IEmailValidatorService _emailValidatorService;

    public DoctorService(
        ICrmValidator crmValidator,
        IPasswordHasher passwordHasher,
        IDoctorRepository doctorRepository,
        ICpfValidatorService cpfValidatorService,
        IEmailValidatorService emailValidatorService)
    {
        _crmValidator = crmValidator;
        _passwordHasher = passwordHasher;
        _doctorRepository = doctorRepository;
        _cpfValidatorService = cpfValidatorService;
        _emailValidatorService = emailValidatorService;
    }

    
    
    public async Task<CreateDoctorDto> CreateDoctor(CreateDoctorDto doctorDto)
    {
        if (await _emailValidatorService.EmailAlreadyRegistered(doctorDto.email))
            throw new ArgumentException($"Email {doctorDto.email} is already registered");
            
        if( await _cpfValidatorService.CpfAlreadyRegistered(doctorDto.cpf))
            throw new ArgumentException($"CPF {doctorDto.cpf} is already registered");
            
        if( await _crmValidator.CrmAlreadyRegistered(doctorDto.crm))
            throw new ArgumentException($"Crm {doctorDto.crm} is already registered");
        
        var passwordHash = _passwordHasher.HashPassword(doctorDto.password);

        var newDoctor = new Doctor
        {
            firstName = doctorDto.firstName,
            lastName = doctorDto.lastName,
            cpf = doctorDto.cpf,
            birthDate = doctorDto.birthDate,
            phone = doctorDto.phone,
            street = doctorDto.street,
            district = doctorDto.district,
            city = doctorDto.city,
            complement = doctorDto.complement,
            typeUser = TypeUser.doctor,
            email = doctorDto.email,
            password = passwordHash,
            crm = doctorDto.crm,
            areaSpecialty = doctorDto.areaSpecialty,
            createdAt = DateTime.UtcNow,
            updatedAt = null
        };
        
        await _doctorRepository.CreateDoctor(newDoctor);
        return doctorDto;
        
    }
}