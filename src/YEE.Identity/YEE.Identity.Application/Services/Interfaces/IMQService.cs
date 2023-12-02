using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace YEE.Identity.Application.Services.Interfaces
{
    public interface IMQService
    {
        void CreateConsumer(string queue, EventHandler<BasicDeliverEventArgs> handler);
        void CreateQueue(string name);
        void SendMessage(string queue, string message);
        void SendMessage(string queue, int message);
    }
}