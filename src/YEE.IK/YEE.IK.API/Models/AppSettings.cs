namespace YEE.IK.API.Models
{
    public class RabbitMQ
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Crypto
    {
        public string HashKey { get; set; }
    }

    public class AppSettings
    {
        public string JWTKey { get; set; }
        public Crypto Crypto { get; set; }
        public RabbitMQ RabbitMQ { get; set; }

    }
}
