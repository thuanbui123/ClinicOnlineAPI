using ClinicOnline.Infrastructure.Interfaces;
using RabbitMQ.Client;

namespace ClinicOnline.Infrastructure.MessageBrokers;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private readonly IConnection _connection;
    private readonly ConnectionFactory _factory;

    public RabbitMqConnection()
    {
        _factory = new ConnectionFactory()
        {
            HostName = "localhost", // sửa lại nếu khác
            UserName = "guest",
            Password = "guest"
        };
        _connection = _factory.CreateConnection();
    }

    public IModel CreateModel()
    {
        return _connection.CreateModel();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
