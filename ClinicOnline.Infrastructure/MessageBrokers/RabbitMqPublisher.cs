using ClinicOnline.Core.Interfaces;
using ClinicOnline.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ClinicOnline.Infrastructure.MessageBrokers;

public class RabbitMqPublisher : IEventPublisher
{
    private readonly IModel _channel;
    private readonly ILogger<RabbitMqPublisher> _logger;

    public RabbitMqPublisher(IRabbitMqConnection connection, ILogger<RabbitMqPublisher> logger)
    {
        _channel = connection.CreateModel();
        _logger = logger;
    }

    /// <summary>
    /// Gửi sự kiện ra exchange thông qua RabbitMQ.
    /// </summary>
    public Task PublishAsync<TEvent>(TEvent @event, string routingKey) where TEvent : class
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

        // Gửi message đến exchange (tên mặc định là clinic_exchange)
        _channel.BasicPublish(
            exchange: "clinic_exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body);

        _logger.LogInformation("Đã publish sự kiện: {Event}", typeof(TEvent).Name);
        return Task.CompletedTask;
    }
}
