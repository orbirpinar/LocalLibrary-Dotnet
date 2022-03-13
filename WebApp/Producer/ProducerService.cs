using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
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

        private const string QueueName = "searchDataQueue";

        public ProducerService(IRabbitMqService rabbitMqService)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare("searchDataExchange", ExchangeType.Topic, durable: true, autoDelete: false);
            _model.QueueBind(QueueName, "searchDataExchange", "searchData");
        }

        public bool Send(SearchParamDto searchParamDto)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(searchParamDto));
                var properties = _model.CreateBasicProperties();
                _model.BasicPublish("searchDataExchange", "searchData", properties, body);
                return true;
            }
            catch (Exception )
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