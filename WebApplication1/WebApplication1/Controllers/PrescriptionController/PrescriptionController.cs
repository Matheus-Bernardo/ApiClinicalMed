using Microsoft.AspNetCore.Mvc;
using WebApplication1.Core.Entities;
using WebApplication1.Services.PrescriptionService;

namespace WebApplication1.Controllers.PrescriptionController;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController:ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> createPrescription(Prescription prescription)
    {
        var prescriptionCreated = await _prescriptionService.createPrescription(prescription);
        return Ok(prescriptionCreated);
    }
}