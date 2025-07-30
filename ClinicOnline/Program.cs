using ClinicOnline.Core;
using ClinicOnline.Core.DTOs;
using ClinicOnline.Core.Enums;
using ClinicOnline.Core.Exceptions;
using ClinicOnline.Infrastructure;
using ClinicOnline.Infrastructure.DatabaseContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .WriteTo.Console() // Ghi log ra console
                .WriteTo.File(Path.Combine("Logs", "error-.logs"), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                    rollingInterval: RollingInterval.Day, // T?o file m?i m?i ngày
                    retainedFileCountLimit: 7) // Gi? t?i ?a 7 file log
                .CreateLogger();
Log.CloseAndFlush();

// 1. Bind c?u hình JWT t? appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// 2. ??ng ký Authentication s? d?ng JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

// 3. C?u hình policy phân quy?n theo claim
builder.Services.AddAuthorization(options =>
{
    // Policy ch? cho phép ng??i dùng có Role = Admin truy c?p
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole(Roles.Admin.ToString()));

    // Policy ch? cho phép ng??i dùng có Role = Doctor truy c?p
    options.AddPolicy("DoctorOnly", policy =>
        policy.RequireRole(Roles.Doctor.ToString()));

    // Policy ch? cho phép ng??i dùng có Role = Patient truy c?p
    options.AddPolicy("PatientOnly", policy =>
        policy.RequireRole(Roles.Patient.ToString()));
});

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddCore(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ClinicManagementContext>(options =>
    options.UseNpgsql(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HandleExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
