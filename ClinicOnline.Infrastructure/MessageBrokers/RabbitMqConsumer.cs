using ClinicOnline.Core.EventHandlers;
using ClinicOnline.Core.Interfaces;
using ClinicOnline.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ClinicOnline.Infrastructure.MessageBrokers;

/// <summary>
/// Triển khai của IEventConsumer sử dụng RabbitMQ.
/// Dùng để lắng nghe các sự kiện từ queue và xử lý chúng.
/// </summary>
public class RabbitMqConsumer : IEventConsumer
{
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RabbitMqConsumer> _logger;

    public RabbitMqConsumer(IRabbitMqConnection connection, ILogger<RabbitMqConsumer> logger, IServiceProvider serviceProvider)
    {
        _channel = connection.CreateModel();
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Đăng ký handler để xử lý message nhận được từ queue.
    /// </summary>
    public void Subscribe<TEvent>(string queueName, Func<TEvent, Task> handler) where TEvent : class
    {
        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = JsonConvert.DeserializeObject<TEvent>(message);

            if (@event != null)
            {
                await handler(@event);
            }

            _channel.BasicAck(ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }
}
