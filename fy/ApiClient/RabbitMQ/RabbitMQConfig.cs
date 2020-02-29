using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.RabbitMQ
{
    public class RabbitMQConfig
    {
        static RabbitMQConfig()
        {
            RabbitMQServerUrl = "amqp://192.168.0.135:5672/";
            RabbitMQUserName = "admin";
            RabbitMQPassword = "xxxxxx";
        }

        public static readonly string RabbitMQServerUrl;
        public static readonly string RabbitMQUserName;
        public static readonly string RabbitMQPassword;
        
        public static readonly string QueueName = "logQueue";
        public static readonly string ExchangeName = "logExchange";
        public static readonly string ExchangeType = "fanout";//topic、fanout
        public static readonly string RoutingKey = "*";

        public static ConnectionFactory NewConnectionFactory()
        {
            var factory = new ConnectionFactory
            {
                Endpoint = new AmqpTcpEndpoint(new Uri(RabbitMQConfig.RabbitMQServerUrl)),
                UserName = RabbitMQConfig.RabbitMQUserName,
                Password = RabbitMQConfig.RabbitMQPassword,
                RequestedHeartbeat = 0
            };
            return factory;
        }
    }
}
