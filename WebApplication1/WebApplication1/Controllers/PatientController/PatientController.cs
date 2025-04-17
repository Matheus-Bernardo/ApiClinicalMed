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
        try
        {
            var patientCreated = await _patientService.CreatePatient(patientDto);
            return CreatedAtAction("CreatePatient", patientCreated);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500,"Erro interno:" + e.Message);
        }
        
    }
    
}