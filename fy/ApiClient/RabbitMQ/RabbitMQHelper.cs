using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiClient.RabbitMQ
{
    public class RabbitMQHelper
    {
        // 生产消息
        public static void Producer(string message)
        {
            try
            {
                var factory = RabbitMQConfig.NewConnectionFactory();
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        ////设置交换器的类型
                        //channel.ExchangeDeclare(RabbitMQConfig.ExchangeName, RabbitMQConfig.ExchangeType);
                        ////绑定消息队列，交换器，routingkey
                        //channel.QueueBind(RabbitMQConfig.QueueName, RabbitMQConfig.ExchangeName, RabbitMQConfig.RoutingKey);

                        //声明一个队列(不存在则自动创建)，队列持久化durable，连接排他性exclusive，队列自动删除(最后连接断开时)autoDelete
                        channel.QueueDeclare(RabbitMQConfig.QueueName, true, false, false, null);
                        //队列持久化
                        var properties = channel.CreateBasicProperties();                        
                        properties.Persistent = true;
                        //发送信息 //注意路由键在用direct交换器时，要指定为队列名
                        var body = Encoding.UTF8.GetBytes(message);
                        //channel.BasicPublish(RabbitMQConfig.ExchangeName, RabbitMQConfig.RoutingKey, properties, body);
                        channel.BasicPublish("", RabbitMQConfig.QueueName, properties, body);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 消费消息-发布订阅
        public static void ConsumerEventingBasic()
        {
            try
            {
                var factory = RabbitMQConfig.NewConnectionFactory();
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //channel.ExchangeDeclare(RabbitMQConfig.ExchangeName, RabbitMQConfig.ExchangeType);
                        //channel.QueueBind(RabbitMQConfig.QueueName, RabbitMQConfig.ExchangeName, RabbitMQConfig.RoutingKey);
                        channel.QueueDeclare(RabbitMQConfig.QueueName, true, false, false, null);
                        
                        //方法中参数prefetchSize为预取的长度，一般设置为0即可，表示长度不限；prefetchCount表示预取的条数，即发送的最大消息条数；global表示是否在Connection中全局设置，true表示Connetion下的所有channel都设置为这个配置
                        channel.BasicQos(prefetchSize: 0, prefetchCount: 2, global: false);

                        //定义这个队列的消费者
                        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);                        

                        //接收到消息时执行的任务
                        consumer.Received += (model, ea) =>
                        {
                            var message = Encoding.UTF8.GetString(ea.Body);
                            //处理完成，手动确认
                            channel.BasicAck(ea.DeliveryTag, false);
                            Console.WriteLine($"处理消息【{message}】完成");                            
                        };

                        //调用消费方法 queue指定消费的队列，ack应答机制 false为手动应答，true为自动应答
                        channel.BasicConsume(RabbitMQConfig.QueueName, false, consumer);

                        Console.WriteLine("消费者准备就绪....");
                        Console.ReadKey();                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 消费消息-主动获取
        public static void ConsumerGet()
        {
            try
            {
                var factory = RabbitMQConfig.NewConnectionFactory();
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //方法中参数prefetchSize为预取的长度，一般设置为0即可，表示长度不限；prefetchCount表示预取的条数，即发送的最大消息条数；global表示是否在Connection中全局设置，true表示Connetion下的所有channel都设置为这个配置
                        channel.BasicQos(prefetchSize: 0, prefetchCount: 2, global: false);
                        channel.ExchangeDeclare(RabbitMQConfig.ExchangeName, RabbitMQConfig.ExchangeType);
                        channel.QueueBind(RabbitMQConfig.QueueName, RabbitMQConfig.ExchangeName, RabbitMQConfig.RoutingKey);
                        channel.QueueDeclare(RabbitMQConfig.QueueName, true, false, false, null);
                        Console.WriteLine("消费者准备就绪....按下任意键获取一个消息");

                        while (true)
                        {
                            Console.ReadKey();
                            BasicGetResult result = channel.BasicGet(RabbitMQConfig.QueueName, true);
                            if (result != null)
                            {
                                var message = Encoding.UTF8.GetString(result.Body);
                                Console.WriteLine($"处理消息【{message}】完成");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
