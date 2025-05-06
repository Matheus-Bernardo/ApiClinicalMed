using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize(Roles = "doctor")]
    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto doctorDto)
    {
        var doctortCreated = await _doctorService.CreateDoctor(doctorDto);
        return CreatedAtAction("CreateDoctor", doctortCreated);
    }
    
    [Authorize(Roles = "doctor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto doctorDto, int id)
    {
        var doctorUpdated = await _doctorService.UpdateDoctor(doctorDto, id);
        return Ok("doctor Updated");
    }
    
    [Authorize(Roles = "doctor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        await _doctorService.DeleteDoctor(id);
        return NoContent();
    }
    
    [Authorize(Roles = "doctor")]
    [HttpGet]
    public async Task<ActionResult<List<DoctorResponseDto>>> GetAllDoctors()
    {
        var doctors = await _doctorService.GetAllDoctors();
        return Ok(doctors);
    }
    
    [Authorize(Roles = "doctor")]
    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorResponseDto>> GetDoctorById(int id)
    {
        var doctors = await _doctorService.GetDoctorById(id);
        return Ok(doctors);
    }
    
}