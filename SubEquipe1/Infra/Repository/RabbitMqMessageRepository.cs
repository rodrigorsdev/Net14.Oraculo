using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SubEquipe1.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubEquipe1.Infra.Repository
{
    public class RabbitMqMessageRepository : DefaultBasicConsumer
    {
        private readonly IModel _channel;

        public RabbitMqMessageRepository(IModel channel)
        {
            _channel = channel;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            string message = Encoding.UTF8.GetString(body);
        }
    }
}
