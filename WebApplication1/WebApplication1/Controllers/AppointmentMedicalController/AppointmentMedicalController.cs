using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Core.Entities;
using WebApplication1.Services.TypeAppointmentMedicalService;

namespace WebApplication1.Controllers.AppointmentMedicalController;

[ApiController]
[Route("api/[controller]")]
public class AppointmentMedicalController:ControllerBase
{
    private readonly ITypeAppointmentService _typeAppointmentService;

    public AppointmentMedicalController(ITypeAppointmentService typeAppointmentService)
    {
        this._typeAppointmentService = typeAppointmentService;
    }

    [Authorize(Roles = "doctor")]
    [HttpPost]
    public async Task<IActionResult> createAppointment([FromBody] TypeAppointmentMedical typeAppointmentMedical)
    {
        var typeAppointmentCreated = await _typeAppointmentService.createTypeAppointmentMedical(typeAppointmentMedical);
        return CreatedAtAction("createAppointment", typeAppointmentCreated);
        
    }
}