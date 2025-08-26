using WebApplication1.DTOS.Consultation;
using WebApplication1.Services.MettingService;

public class MeetingService : IMeetingService
{
    private const string JitsiBaseUrl = "https://meet.jit.si/";

    public Task<string> GenerateMeetingLink(ResponseCreateConsultation dto)
    {
        string roomName = GenerateRoomName(dto);
        string meetingLink = $"{JitsiBaseUrl}{roomName}";
        return Task.FromResult(meetingLink);
    }

    private string GenerateRoomName(ResponseCreateConsultation dto)
    {
        string paciente = Sanitize(dto.namePatient);
        string medico = Sanitize(dto.nameDoctor);
        string dataHora = dto.consultationTime.ToString("yyyyMMddTHHmm");

        return $"consult_{paciente}_{medico}_{dataHora}";
    }

    private static string Sanitize(string input)
    {
        return string.Concat(input.Where(char.IsLetterOrDigit));
    }
}