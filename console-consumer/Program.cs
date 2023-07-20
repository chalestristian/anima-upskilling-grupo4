using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace console_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "service-rabbitmq", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "fila_boletos", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    ProcessarMatricula(message);
                };

                var consumerTag = channel.BasicConsume(queue: "fila_boletos", autoAck: true, consumer: consumer);

                Console.WriteLine("O consumidor está ouvindo a fila. Pressione [Ctrl + C] para sair.");
                Console.CancelKeyPress += (sender, e) =>
                {
                    Console.WriteLine("Desligando o consumidor...");
                    channel.BasicCancel(consumerTag);
                };

                // Mantém o consumidor em execução
                while (true){}
            }
        }

        static void ProcessarMatricula(Object message)
        {
            Console.WriteLine("Mensagem recebida: {0}", message);
        }
    }
}
