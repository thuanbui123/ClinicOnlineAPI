using ClinicOnline.Core.EventHandlers;
using ClinicOnline.Core.Interfaces;
using ClinicOnline.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicOnline.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<AppointmentCreatedHandler>();

        services.AddScoped<IEmailService, EmailService>();
        
        return services;
    }
}
