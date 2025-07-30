using RabbitMQ.Client;

namespace ClinicOnline.Infrastructure.Interfaces;

public interface IRabbitMqConnection
{
    IModel CreateModel();
}
