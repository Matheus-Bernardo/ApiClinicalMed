using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOS.Consultation;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Services.ConsultationService;

namespace WebApplication1.Controllers.ConsultationMedicalController;

[ApiController]
[Route("api/[controller]")]
public class ConsultationMedicalController:ControllerBase
{
    private readonly IConsultationService _consultationService;

    public ConsultationMedicalController(IConsultationService consultationService)
    {
        _consultationService = consultationService;
    }

    [Authorize(Roles = "doctor")]
    [HttpPost]
    public async Task<IActionResult> createConsultationMedical([FromBody] CreateConsultationDto consultation)
    {
        var consultationMedicalCreated = await _consultationService.createConsultation(consultation);
        return CreatedAtAction("createConsultationMedical", consultationMedicalCreated);
        
    }
}