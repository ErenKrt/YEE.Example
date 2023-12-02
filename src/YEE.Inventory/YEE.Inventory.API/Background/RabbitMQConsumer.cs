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
            var deletedConsumer = new EventingBasicConsumer(_channel);
            deletedConsumer.Received += async (ch, ea) =>
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

            _channel.BasicConsume("User.Deleted", false, deletedConsumer);

            var createdConsumer = new EventingBasicConsumer(_channel);
            createdConsumer.Received += async(ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);
                var userID = Convert.ToInt32(content);

                _channel.BasicAck(ea.DeliveryTag, false);

                using (var scope = _services.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IItemService>();
                    await service.Create(new()
                    {
                        Name= "First Item",
                        OwnerID=userID,
                        Quantity= "1"
                    });
                }
            };
            _channel.BasicConsume("User.Created", false, createdConsumer);

            return Task.CompletedTask;
        }
    }
}
