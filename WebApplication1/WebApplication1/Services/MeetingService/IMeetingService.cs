using WebApplication1.DTOS.Consultation;

namespace WebApplication1.Services.MettingService;

public interface IMeetingService
{
    Task<string> GenerateMeetingLink(ResponseCreateConsultation dto);

}