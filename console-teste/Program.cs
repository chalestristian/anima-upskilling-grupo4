using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using api.Models;

namespace console_teste
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 56721 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "fila_boletos", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var matricula = new Matricula { Id = 1, ValorMatricula = 50 };

                var matriculaBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(matricula));
                channel.BasicPublish(exchange: "", routingKey: "fila_boletos", basicProperties: null, body: matriculaBytes);
                Console.WriteLine("Objeto Matricula enviado para a fila.");

                Console.WriteLine("Pressione qualquer tecla para sair.");
                Console.ReadKey();
            }
        }
    }
}
