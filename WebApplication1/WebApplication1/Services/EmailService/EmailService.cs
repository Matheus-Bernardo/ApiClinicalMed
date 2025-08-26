using MailKit.Net.Smtp;
using MimeKit;
using WebApplication1.DTOS.Consultation;


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
        Console.WriteLine($"Sender:{sender}");
        _logger.LogInformation("Sender: {sender}", sender);
        
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

        Console.WriteLine($"emailPatient: {emailDto.emailPatient}");
        Console.WriteLine($"emailDoctor: {emailDto.emailDoctor}");

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587,MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(sender, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}