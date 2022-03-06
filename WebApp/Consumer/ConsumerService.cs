using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApp.Commands;
using WebApp.Seeder;

namespace WebApp.Consumer
{
    
   public interface IConsumerService
    {
        Task ReadMessages();
    }

    public class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        
        private const string QueueName = "seedDataQueue";
        
        public ConsumerService(IRabbitMqService rabbitMqService, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare("seedDataExchange", ExchangeType.Topic, durable: true, autoDelete: false);
            _model.QueueBind(QueueName, "seedDataExchange", "seedDataRoutingKey");
        }

        public async Task ReadMessages()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (_, args) =>
            {
                var body = args.Body.ToArray();
                var jsonData = Encoding.UTF8.GetString(body);
                Console.WriteLine(jsonData);
                var seedData = JsonConvert.DeserializeObject<List<Dto.SeedData>>(jsonData);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(new PopulateDatabase(seedData));
                }
                
                
                
                await Task.CompletedTask;
                _model.BasicAck(args.DeliveryTag, false);
            };
            _model.BasicConsume(QueueName, false, consumer);
            await Task.CompletedTask;
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