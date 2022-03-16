using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using WebApp.Configuration;
using WebApp.Dto;

namespace WebApp.Producer
{
    public interface IProducerService
    {
        bool Send(SearchParamDto searchParamDto);
    }

    public class ProducerService : IProducerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        private readonly ILogger<ProducerService> _logger;

        private const string QueueName = "searchDataQueue";

        public ProducerService(IRabbitMqService rabbitMqService, ILogger<ProducerService> logger)
        {
            _logger = logger;
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(QueueName, true, false, false);
            _model.ExchangeDeclare("searchDataExchange", ExchangeType.Topic, true);
            _model.QueueBind(QueueName, "searchDataExchange", "searchData");
        }

        public bool Send(SearchParamDto searchParamDto)
        {
            try
            {
                var policy = Policy.Handle<BrokerUnreachableException>()
                    .Or<SocketException>()
                    .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        (exception, span) => { _logger.LogWarning(exception, "Could not publish event:  after {Timeout}s ({ExceptionMessage})", $"{span.TotalSeconds:n1}", exception.Message); });
                policy.Execute(() =>
                {
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(searchParamDto));
                    var properties = _model.CreateBasicProperties();
                    _model.BasicPublish("searchDataExchange", "searchData", properties, body);
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}