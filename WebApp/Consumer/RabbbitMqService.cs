using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace WebApp.Consumer
{
public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }

    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new()
            {
                UserName = _configuration.Username,
                Password = _configuration.Password,
                HostName = _configuration.HostName,
                DispatchConsumersAsync = true
            };
            var channel = connection.CreateConnection();
            return channel;
        }
    }

}