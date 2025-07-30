using ClinicOnline.Core.Events;
using ClinicOnline.Core.Interfaces;
using Microsoft.Extensions.Hosting;

namespace ClinicOnline.Infrastructure.MessageBrokers;

public class EventBusBackgroundService : BackgroundService
{
    private readonly IEventConsumer _consumer;

    public EventBusBackgroundService(IEventConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        _consumer.Subscribe<AppointmentCreatedEvent>("appointment-created-queue", async (evt) =>
        {
            Console.WriteLine($"[EVENT] Đã nhận event AppointmentCreated: {evt.AppointmentId}, {evt.Time}");
            await Task.CompletedTask;
        });

        // Có thể đăng ký thêm nhiều event khác ở đây
        return Task.CompletedTask;
    }
}
