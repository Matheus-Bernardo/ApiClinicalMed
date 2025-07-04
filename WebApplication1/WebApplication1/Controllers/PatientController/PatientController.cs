using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOS.Patient;
using WebApplication1.Services.PatientService;
namespace WebApplication1.Controllers.PatientController;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    [HttpPost]
    public async Task< IActionResult> CreatePatient([FromBody] CreatePatientDto patientDto)
    {
        var patientCreated = await _patientService.CreatePatient(patientDto);
        return CreatedAtAction("CreatePatient", patientCreated);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientDto  patientDto,  int id)
    {
        var patientUpdated = await _patientService.UpdatePatient(patientDto, id);
        return Ok("Patient Updated");

    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        await _patientService.DeletePatient(id);
        return NoContent();
    }
    [HttpGet]
    public async Task<ActionResult<List<PatientResponseDto>>> GetAllPatients()
    {
        var patients = await _patientService.GetAllPatient();
        return Ok(patients);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponseDto>> GetPatientById(int id)
    {
            var patient = await _patientService.GetPatientById(id);
            return Ok(patient);
    }
    
}