using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using YEE.Inventory.API.Models;
using YEE.Inventory.API.Services;

namespace YEE.Inventory.API.Background
{
    public class RabbitMQConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private AppSettings _appSettings;
        private IServiceProvider _services { get; }

        public RabbitMQConsumer(IOptions<AppSettings> options, IServiceProvider services)
        {
            _appSettings = options.Value;
            _services = services;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { 
                HostName= _appSettings.RabbitMQ.Host,
                UserName= _appSettings.RabbitMQ.Username,
                Password = _appSettings.RabbitMQ.Password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                // received message  
                var body = ea.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);

                var userID = Convert.ToInt32(content);

                using (var scope = _services.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IItemService>();
                    await service.DeleteFromUserID(userID);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("User.Deleted", false, consumer);

            return Task.CompletedTask;
        }
    }
}
