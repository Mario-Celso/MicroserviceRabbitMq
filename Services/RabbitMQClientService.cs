using MicroserviceRabbitMq.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace MicroserviceRabbitMq.Services
{
    public class RabbitMQClientService
    {
        private readonly RabbitMQSettings _settings;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQClientService(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
            var factory = new ConnectionFactory
            {
                //HostName = _settings.HostName,
                //UserName = _settings.UserName,
                //Password = _settings.Password

                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendMessage(string queue, string message)
        {
            _channel.QueueDeclare(queue: queue,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: queue,
                                  basicProperties: null,
                                  body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
