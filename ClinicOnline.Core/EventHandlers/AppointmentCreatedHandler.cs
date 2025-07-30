using ClinicOnline.Core.Events;
using ClinicOnline.Core.Interfaces;
namespace ClinicOnline.Core.EventHandlers;

public class AppointmentCreatedHandler : IEventHandler<AppointmentCreatedEvent>
{
    private readonly IEventPublisher _eventPublisher;

    public AppointmentCreatedHandler(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public async Task HandleAsync(AppointmentCreatedEvent @event)
    {
        await _eventPublisher.PublishAsync(new AppointmentCreatedEvent
        {
            AppointmentId = Guid.NewGuid(),
            PatientId = Guid.NewGuid(),
            DoctorId = Guid.NewGuid(),
            Time = DateTime.Now.AddHours(1),
            Note = "Khám tổng quát",
            Status = "Confirmed"
        }, "appointment-created-queue");
    }
}
