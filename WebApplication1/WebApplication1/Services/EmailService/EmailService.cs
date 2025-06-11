using MailKit.Net.Smtp;
using MimeKit;
using WebApplication1.DTOS.Consultation;


namespace WebApplication1.Services.EmailService;

public class EmailService: IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public EmailService(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }
    
    public async Task SendAppointmentEmail(ResponseCreateConsultation appointmentEmail)
    {
        var sender = _configuration["EmailSettings:sender"];
        var password = _configuration["EmailSettings:password"];
        
        var templatePath = Path.Combine(AppContext.BaseDirectory, "Services/EmailService/templates", "AppointmentEmail.html");
        var templateHtml = await File.ReadAllTextAsync(templatePath);

        var messagehtml = templateHtml
            .Replace("{namePatient}", appointmentEmail.namePatient)
            .Replace("{consultationLink}",appointmentEmail.consultationLink)
            .Replace("{consultationTime}", appointmentEmail.consultationTime.ToString("dd/MM/yyyy HH:mm"))
            .Replace("{nameDoctor}", appointmentEmail.nameDoctor);
        
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Telenfermagem",sender));
        email.To.Add(MailboxAddress.Parse(appointmentEmail.emailPatient));
        email.To.Add(MailboxAddress.Parse(appointmentEmail.emailDoctor));
        email.Subject = "Telenfermagem - Consulta Marcada";
        email.Body = new TextPart("html"){Text = messagehtml}; 

        Console.WriteLine($"emailPatient: {appointmentEmail.emailPatient}");
        Console.WriteLine($"emailDoctor: {appointmentEmail.emailDoctor}");

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587,MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(sender, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}