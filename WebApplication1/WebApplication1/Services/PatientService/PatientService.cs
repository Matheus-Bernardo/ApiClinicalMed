using Microsoft.AspNetCore.Mvc;
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
    private readonly FindUser _findUser;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPatientRepository _patientRepository;
    private readonly ICpfValidatorService _cpfValidatorService;
    private readonly IEmailValidatorService _emailValidatorService;
    private readonly ISusCardValidatorService _susCardValidatorService;

    public PatientService(
        FindUser findUser,
        IPasswordHasher passwordHasher,
        IPatientRepository patientRepository,
        ICpfValidatorService cpfValidatorService,
        IEmailValidatorService emailValidatorService,
        ISusCardValidatorService susCardValidatorService)
    {
        _findUser = findUser;
        _passwordHasher = passwordHasher;
        _patientRepository = patientRepository;
        _cpfValidatorService = cpfValidatorService;
        _emailValidatorService = emailValidatorService;
        _susCardValidatorService = susCardValidatorService;
    }
    
    public async Task<PatientResponseDto> CreatePatient(CreatePatientDto patientDto)
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
        
        var createdPatient =  await _patientRepository.AddPatient(newPatient);
        return new PatientResponseDto
        {
            Id = createdPatient.Id,
            FirstName = createdPatient.firstName,
            LastName = createdPatient.lastName,
            Email = createdPatient.email,
            SusCard = createdPatient.susCard,
            Phone = createdPatient.phone,
            City = createdPatient.city
        };

    }

    public async Task<UpdatePatientDto> UpdatePatient(UpdatePatientDto patient,int id)
    {
        var findPatient = await _findUser.FindPatientById(id);
       
        if(findPatient == null)
            throw new ArgumentException($"User with id {id} not found");
        
        if(await _cpfValidatorService.CpfAlreadyRegistered(patient.cpf))
            throw new ArgumentException($"cpf {patient.cpf} is already registered, belongs to someone else ");
        
        if (await _susCardValidatorService.SusCardAlreadyRegistered(patient.susCard))
            throw new ArgumentException($"sus card {patient.susCard} is already registered,belongs to someone else");
        
        if(await _emailValidatorService.EmailAlreadyRegistered(patient.email))
            throw new ArgumentException($"email {patient.email} is already registered,belongs to someone else");
        
        try
        {
            findPatient.firstName = patient.firstName??findPatient.firstName;
            findPatient.lastName = patient.lastName??findPatient.lastName;
            findPatient.cpf = patient.cpf??findPatient.cpf;
            findPatient.birthDate = patient.birthDate??findPatient.birthDate;
            findPatient.phone = patient.phone??findPatient.phone;
            findPatient.street = patient.street??findPatient.street;
            findPatient.district = patient.district??findPatient.district;
            findPatient.city = patient.city??findPatient.city;
            findPatient.complement = patient.complement??findPatient.complement;
            findPatient.email = patient.email??findPatient.email;
            findPatient.password = patient.password??findPatient.password;
            findPatient.susCard = patient.susCard??findPatient.susCard;
            findPatient.messagePhone = patient.messagePhone??findPatient.messagePhone;
            findPatient.familyHistoryDisease = patient.familyHistoryDisease ?? findPatient.familyHistoryDisease;
            findPatient.medicalAgreements = patient.medicalAgreements ?? findPatient.medicalAgreements;
        
            await _patientRepository.UpdatePatient(findPatient);
            return patient;
        }catch (Exception e)
        {
            throw new ArgumentException($"Ocorreu uma falha desconhecida para atualizar!", e);
        }
        

    }

    public async Task<List<PatientResponseDto>> GetAllPatient()
    {
        var patients = await _patientRepository.GetAllPatients();

        return patients.Select(p => new PatientResponseDto
        {
            Id = p.Id,
            FirstName = p.firstName,
            LastName = p.lastName,
            Email = p.email,
            SusCard = p.susCard,
            Phone = p.phone,
            City = p.city,
        }).ToList();
    }

    public async Task<PatientResponseDto?> GetPatientById(int id)
    {
        var patient = await _patientRepository.GetPatientById(id);
        
        if(patient == null)
            throw new ArgumentException($"User with id {id} not found");

        return new PatientResponseDto
        {
            Id = patient.Id,
            FirstName = patient.firstName,
            LastName = patient.lastName,
            Email = patient.email,
            SusCard = patient.susCard,
            Phone = patient.phone,
            City = patient.city,
            District = patient.district,
            Street= patient.street,
            Complement = patient.complement

        };

    }

    public async Task<bool> DeletePatient(int id)
    {
        var patiendDeleted = await _findUser.FindPatientById(id);
        if(patiendDeleted == null)
            throw new ArgumentException($"User with id {id} not found");
        try
        {
            await _patientRepository.DeletePatient(patiendDeleted);
            return true;
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException("Ocorreu uma falha para deletar:", e.Message);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Erro do servidor:"+ e.Message);
        }
    }
}