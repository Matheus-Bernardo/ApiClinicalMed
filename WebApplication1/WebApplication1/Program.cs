using Microsoft.EntityFrameworkCore;
using WebApplication1.Extensions;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Services.LoginService;
using WebApplication1.Utils;

var builder = WebApplication.CreateBuilder(args);

//connection Bd
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//services
builder.Services.AddApplicationPatientServices();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<FindUser>();

//Ccontrollers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();