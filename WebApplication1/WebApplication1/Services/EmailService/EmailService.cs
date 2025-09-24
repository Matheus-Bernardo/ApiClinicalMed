using MailKit.Net.Smtp;
using MimeKit;
using WebApplication1.DTOS.Consultation;
using WebApplication1.DTOS.Email;
using QuestPDF.Fluent;
using QuestPDF.Helpers;


namespace WebApplication1.Services.EmailService;

public class EmailService: IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration, IWebHostEnvironment env, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        
        _logger = logger;
    }
    
    public async Task SendAppointmentEmail(ResponseCreateConsultation emailDto)
    {
        var sender = _configuration["EmailSettings:sender"];
        var password = _configuration["EmailSettings:password"];
        
        
        var templatePath = Path.Combine(AppContext.BaseDirectory, "Services/EmailService/templates", "AppointmentEmail.html");
        var templateHtml = await File.ReadAllTextAsync(templatePath);
        
        var messagehtml = templateHtml
            .Replace("{namePatient}", emailDto.namePatient)
            .Replace("{consultationLink}",emailDto.consultationLink)
            .Replace("{consultationTime}", emailDto.consultationTime.ToString("dd/MM/yyyy HH:mm"))
            .Replace("{nameDoctor}", emailDto.nameDoctor);
        
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Telenfermagem",sender));
        email.To.Add(MailboxAddress.Parse(emailDto.emailPatient));
        email.To.Add(MailboxAddress.Parse(emailDto.emailDoctor));
        email.Subject = "Telenfermagem - Consulta Marcada";
        email.Body = new TextPart("html"){Text = messagehtml}; 
        
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587,MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(sender, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task SendPrescriptionEmail(int idPrescription, SendPrescriptionEmailDTO emailDto)
{
    var sender = _configuration["EmailSettings:sender"];
    var password = _configuration["EmailSettings:password"];

  
    var templatePath = Path.Combine(AppContext.BaseDirectory, "Services/EmailService/templates", "PrescriptionEmail.html");
    var templateHtml = await File.ReadAllTextAsync(templatePath);

    var remedyList = string.Join("", emailDto.RemedyPrescription.Select(med => $"<li>{med}</li>"));

    var messageHtml = templateHtml
        .Replace("{prescriptionId}", emailDto.PrescriptionId.ToString())
        .Replace("{patientName}", emailDto.PatientName)
        .Replace("{doctorName}", emailDto.DoctorName)
        .Replace("{crmDoctor}", emailDto.CrmDoctor)
        .Replace("{validityPrescription}", emailDto.ValidityPrescription.ToString())
        .Replace("{remedyPrescription}", remedyList)
        .Replace("{frequency}", emailDto.FrequencyRemedy)
        .Replace("{dosageRemedy}", emailDto.DosageRemedy)
        .Replace("{frequencyRemedy}", emailDto.FrequencyRemedy)
        .Replace("{observation}", string.IsNullOrWhiteSpace(emailDto.Observation) ? "Sem observações." : emailDto.Observation)
        .Replace("{createdAt}", emailDto.CreatedAt.ToString("dd/MM/yyyy"));

   
    byte[] pdfBytes;
    using (var stream = new MemoryStream())
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Header().AlignCenter().Text("Receita Médica")
                    .FontSize(22).Bold().FontColor(Colors.Blue.Medium);

                page.Content().PaddingVertical(15).Column(col =>
                {
                    
                    col.Item().Background(Colors.Grey.Lighten4).Padding(10).Column(info =>
                    {
                        info.Item().Text($"Paciente: {emailDto.PatientName}").FontSize(12);
                        info.Item().Text($"Médico: {emailDto.DoctorName}").FontSize(12);
                        info.Item().Text($"CRM: {emailDto.CrmDoctor}").FontSize(12);
                        info.Item().Text($"Validade: {emailDto.ValidityPrescription} dias").FontSize(12);
                    });

                    col.Spacing(15);

                 
                    col.Item().Text("Medicamentos Prescritos:").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    foreach (var med in emailDto.RemedyPrescription)
                        col.Item().Text($"- {med}").FontSize(12);

                    col.Spacing(10);
                    
                    col.Item().Background(Colors.Grey.Lighten4).Padding(10).Column(info =>
                    {
                        info.Item().Text($"Dosagem: {emailDto.DosageRemedy}").FontSize(12);
                        info.Item().Text($"Frequência: {emailDto.FrequencyRemedy}").FontSize(12);
                    });

                    col.Spacing(10);
                    
                    col.Item().Text("Observações:").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    col.Item().Text(string.IsNullOrWhiteSpace(emailDto.Observation) ? "Sem observações." : emailDto.Observation).FontSize(12);

                    col.Spacing(20);
                    
                    col.Item().AlignRight().Text("Assinado via Certificado Digital").Bold().FontSize(12);
                    col.Item().AlignRight().Text("Assinatura do Médico").FontSize(10).Italic().FontColor(Colors.Grey.Darken2);
                });

                page.Footer().AlignRight().Text($"Emitido em: {emailDto.CreatedAt:dd/MM/yyyy}").FontSize(10).FontColor(Colors.Grey.Darken2);
            });
        }).GeneratePdf(stream);

        pdfBytes = stream.ToArray();
    }


    var email = new MimeMessage();
    email.From.Add(new MailboxAddress("Telenfermagem", sender));
    email.To.Add(MailboxAddress.Parse(emailDto.EmailDoctor));
    email.To.Add(MailboxAddress.Parse(emailDto.EmailPatient));
    email.Subject = $"Receita Médica Nº {emailDto.PrescriptionId}";

    var builder = new BodyBuilder
    {
        HtmlBody = messageHtml
    };
    builder.Attachments.Add($"Receita_{emailDto.PrescriptionId}.pdf", pdfBytes, new ContentType("application", "pdf"));

    email.Body = builder.ToMessageBody();
    
    using var smtp = new SmtpClient();
    await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
    await smtp.AuthenticateAsync(sender, password);
    await smtp.SendAsync(email);
    await smtp.DisconnectAsync(true);
}


}