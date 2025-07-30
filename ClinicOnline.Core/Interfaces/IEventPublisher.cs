namespace ClinicOnline.Core.Interfaces;

/// <summary>
/// Interface cho các lớp có chức năng gửi (publish) sự kiện ra message queue.
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Gửi một sự kiện ra message queue với routing key chỉ định.
    /// </summary>
    /// <typeparam name="TEvent">Loại sự kiện.</typeparam>
    /// <param name="event">Đối tượng sự kiện.</param>
    /// <param name="routingKey">Routing key dùng để định tuyến.</param>
    Task PublishAsync<TEvent>(TEvent @event, string routingKey) where TEvent : class;
}
