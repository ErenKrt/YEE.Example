using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using YEE.Identity.Application.Models;
using YEE.Identity.Application.Services.Interfaces;


namespace YEE.Identity.Application.Services.Impl
{
    public class MQService : IMQService, IDisposable
    {
        private readonly AppSettings _appSettings;
        private ConnectionFactory _connectionFactory;
        private IConnection? _connection;
        private IModel? _channel;

        public MQService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            _connectionFactory = new ConnectionFactory
            {
                HostName = _appSettings.RabbitMQ.Host,
                UserName = _appSettings.RabbitMQ.Username,
                Password = _appSettings.RabbitMQ.Password
            };
        }

        private void EnsureConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (_channel == null || !_channel.IsOpen)
            {
                _channel = _connection.CreateModel();
            }
        }

        public void CreateQueue(string name)
        {
            EnsureConnection();

            _channel.QueueDeclare(
                queue: name,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        public void SendMessage(string queue, string message)
        {
            EnsureConnection();
            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queue,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message)
            );
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
