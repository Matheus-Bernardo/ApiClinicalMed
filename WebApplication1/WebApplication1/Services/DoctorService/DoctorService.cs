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
    
    private readonly FindUser _findUser;
    private readonly ICrmValidator _crmValidator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ICpfValidatorService _cpfValidatorService;
    private readonly IEmailValidatorService _emailValidatorService;

    public DoctorService(
        FindUser findUser,
        ICrmValidator crmValidator,
        IPasswordHasher passwordHasher,
        IDoctorRepository doctorRepository,
        ICpfValidatorService cpfValidatorService,
        IEmailValidatorService emailValidatorService)
    {
        _findUser = findUser;
        _crmValidator = crmValidator;
        _passwordHasher = passwordHasher;
        _doctorRepository = doctorRepository;
        _cpfValidatorService = cpfValidatorService;
        _emailValidatorService = emailValidatorService;
    }

    
    
    public async Task<DoctorResponseDto> CreateDoctor(CreateDoctorDto doctorDto)
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
        
        var doctorCreated = await _doctorRepository.CreateDoctor(newDoctor);
        return new DoctorResponseDto
        {
            Id = doctorCreated.Id,
            FirstName = doctorCreated.firstName,
            LastName = doctorCreated.lastName,
            Crm = doctorCreated.crm,
            AreaSpecialty = doctorCreated.areaSpecialty,
            Email = doctorCreated.email
        };
        
    }

    public async Task<bool> UpdateDoctor(UpdateDoctorDto updateDoctorDto, int idUserUpdated)
    {
        var existingDoctor = await _findUser.FindDoctorById(idUserUpdated); 
        
        if( existingDoctor == null)
            throw new ArgumentException($"Doctor with id {idUserUpdated} not found");
        
        if(await _cpfValidatorService.CpfAlreadyRegistered(updateDoctorDto.cpf))
            throw new ArgumentException($"CPF {updateDoctorDto.cpf} is already registered,belongs to someone else");
        
        if(await _crmValidator.CrmAlreadyRegistered(updateDoctorDto.crm))
            throw new ArgumentException($"Crm {updateDoctorDto.crm} is already registered");
        
        if(await _emailValidatorService.EmailAlreadyRegistered(updateDoctorDto.email))
            throw new ArgumentException($"Email {updateDoctorDto.email} is already registered");

        try
        {
            existingDoctor.firstName = updateDoctorDto.firstName??existingDoctor.firstName;
            existingDoctor.lastName = updateDoctorDto.lastName??existingDoctor.lastName;
            existingDoctor.cpf = updateDoctorDto.cpf??existingDoctor.cpf;
            existingDoctor.birthDate = updateDoctorDto.birthDate??existingDoctor.birthDate;
            existingDoctor.phone = updateDoctorDto.phone??existingDoctor.phone;
            existingDoctor.street = updateDoctorDto.street??existingDoctor.street;
            existingDoctor.district = updateDoctorDto.district??existingDoctor.district;
            existingDoctor.city = updateDoctorDto.city??existingDoctor.city;
            existingDoctor.complement = updateDoctorDto.complement??existingDoctor.complement;
            existingDoctor.email = updateDoctorDto.email??existingDoctor.email;
            existingDoctor.password = updateDoctorDto.password??existingDoctor.password;
            existingDoctor.crm = updateDoctorDto.crm??existingDoctor.crm;
            existingDoctor.areaSpecialty = updateDoctorDto.areaSpecialty??existingDoctor.areaSpecialty;
            existingDoctor.updatedAt = DateTime.UtcNow;
            
            await _doctorRepository.UpdateDoctor(existingDoctor);
            return true;
            
        }
        catch (Exception e)
        {
            throw new ArgumentException("Ocorreu uma falha desconhecida para atualizar a doctor", e);
        }
        
    }

    public async Task<List<DoctorResponseDto>> GetAllDoctors()
    {
        var doctors = await _doctorRepository.getAllDoctors();
        
        if (doctors == null)
            throw new ArgumentException($"List doctors is empty");

        return doctors.Select(doctors => new DoctorResponseDto
        {
            Id = doctors.Id,
            FirstName = doctors.firstName,
            LastName = doctors.lastName,
            Crm = doctors.crm,
            AreaSpecialty = doctors.areaSpecialty,
            Email = doctors.email,

        }).ToList();
    }

    public async Task<DoctorResponseDto> GetDoctorById(int id)
    {
        var doctor = await _doctorRepository.GetDoctorById(id);
        
        if(doctor == null)
            throw new ArgumentException($"Doctor with id {id} not found");

        return new DoctorResponseDto
        {
            Id = doctor.Id,
            FirstName = doctor.firstName,
            LastName = doctor.lastName,
            Crm = doctor.crm,
            AreaSpecialty = doctor.areaSpecialty,
            Email = doctor.email,
        };
    }

    public async Task<bool> DeleteDoctor(int id)
    {
        var doctorDeleted = await _findUser.FindDoctorById(id);
        if (doctorDeleted == null)
            throw new ArgumentException($"Doctor with id {id} not found");

        try
        {
            await _doctorRepository.DeleteDoctor(doctorDeleted);
            return true;
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException("Ocorreu uma falha para deletar:", e.Message);
        }
        catch (Exception e)
        {
            throw new Exception("Erro do servidor:"+ e.Message);
        }
    }
}