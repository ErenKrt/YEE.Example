namespace YEE.Identity.Application.Services.Interfaces
{
    public interface IMQService
    {
        void CreateQueue(string name);
        void SendMessage(string queue, string message);
    }
}