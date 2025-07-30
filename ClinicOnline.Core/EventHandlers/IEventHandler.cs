namespace ClinicOnline.Core.EventHandlers;

public interface IEventHandler<TEvent> where TEvent : class
{
    Task HandleAsync(TEvent @event);
}
