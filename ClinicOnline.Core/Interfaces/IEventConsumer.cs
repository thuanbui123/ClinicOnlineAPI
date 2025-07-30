namespace ClinicOnline.Core.Interfaces;

/// <summary>
/// Interface cho các lớp có chức năng lắng nghe và xử lý sự kiện từ message queue.
/// </summary>
public interface IEventConsumer
{
    /// <summary>
    /// Đăng ký lắng nghe một loại sự kiện và xử lý nó bằng handler tương ứng.
    /// </summary>
    /// <typeparam name="TEvent">Loại sự kiện.</typeparam>
    /// <param name="queueName">Tên queue cần lắng nghe.</param>
    void Subscribe<TEvent>(string queueName, Func<TEvent, Task> handler) where TEvent : class;
}
