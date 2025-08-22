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
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> createConsultationMedical([FromBody] CreateConsultationDto consultation)
    {
        var consultationMedicalCreated = await _consultationService.createConsultation(consultation);
        return CreatedAtAction("createConsultationMedical", consultationMedicalCreated);
        
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllConsultationMedical()
    {
        var consults = await _consultationService.GetMedicalConsultations();
        return Ok(consults);
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetConsultationByUser([FromRoute] int userId)
    {
        try
        {
            var consultsUser = await _consultationService.GetMedicalConsultationsByUserId(userId);
            return Ok(consultsUser);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno no servidor" });
        }
    }

    [Authorize]
    [HttpPut("finishWithPrescription")]
    public async Task<IActionResult> finishConsultationWithPrescription(FinishConsultationDto consultation)
    {
        try
        {
            var consultFinish = await _consultationService.finishMedicalConsultationWithPrescription(consultation);
            return Ok(consultFinish);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
    }
    [Authorize]
    [HttpPut("finishWithoutPrescription")]
    public async Task<IActionResult> finishConsultationWithoutPrescription(FinishConsultationDto consultation)
    {
        try
        {
            var consultFinish = await _consultationService.finishMedicalConsultationWithoutPrescription(consultation);
            return Ok(consultFinish);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = e.Message });
        }
    }
}