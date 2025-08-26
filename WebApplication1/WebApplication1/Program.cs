using Microsoft.EntityFrameworkCore;
using WebApplication1.Extensions;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Services.LoginService;
using WebApplication1.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.SetIsOriginAllowed(origin => origin == "https://clinicalmedweb.web.app" || origin.StartsWith("http://localhost"))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });

});


//connection Bd
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//services
builder.Services.AddApplicationPatientServices();
builder.Services.AddApplicationValidationServices();
builder.Services.AddApplicationConsultationServices();
builder.Services.AddApplicationPrescriptionServices();
builder.Services.AddApplicationTypeConsultationServices();
builder.Services.AddScoped<FindUser>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
builder.Services.AddJwtCollection(builder.Configuration);

//Ccontrollers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowSpecificOrigin");
app.UseAuthentication(); 
app.UseAuthorization();

app.UseGlobalExceptionHandler();

app.MapControllers();
await app.RunAsync();