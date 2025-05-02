using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOS.Doctor;
using WebApplication1.Services.DoctorService;
namespace WebApplication1.Controllers.DoctorController;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto doctorDto)
    {
        try
        {
            var patientCreated = await _doctorService.CreateDoctor(doctorDto);
            return CreatedAtAction("CreateDoctor", patientCreated);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error"+e.Message);
        }
    }
    
    
}