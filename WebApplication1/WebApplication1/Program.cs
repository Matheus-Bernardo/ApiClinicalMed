using Microsoft.EntityFrameworkCore;
using WebApplication1.Extensions;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Services.LoginService;
using WebApplication1.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//connection Bd
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//services
builder.Services.AddApplicationPatientServices();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
builder.Services.AddScoped<FindUser>();
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddJwtCollection(builder.Configuration);

//Ccontrollers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthentication(); 
app.UseAuthorization();

app.UseGlobalExceptionHandler();

app.Run();