using ClinicOnline.Core.EventHandlers;
using ClinicOnline.Core.Events;
using ClinicOnline.Core.Interfaces;
using ClinicOnline.Infrastructure.Interfaces;
using ClinicOnline.Infrastructure.MessageBrokers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicOnline.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        services.AddSingleton<IEventPublisher, RabbitMqPublisher>();
        services.AddSingleton<IEventConsumer, RabbitMqConsumer>();
        services.AddHostedService<EventBusBackgroundService>();
        services.AddScoped<IEventHandler<AppointmentCreatedEvent>, AppointmentCreatedHandler>();
        return services;
    }
}
