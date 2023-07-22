using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace console_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //var factory = new ConnectionFactory() { HostName = "localhost", Port = 56721 };
            var factory = new ConnectionFactory() { HostName = "service-rabbitmq", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "certificado-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    ProcessarMatricula(message, channel, ea.DeliveryTag);
                };

                var consumerTag = channel.BasicConsume(queue: "certificado-queue", autoAck: false, consumer: consumer);

                Console.WriteLine("O consumidor está ouvindo a fila certificado-queue. Pressione [Ctrl + C] para sair.");
                Console.CancelKeyPress += (sender, e) =>
                {
                    Console.WriteLine("Desligando o consumidor...");
                    channel.BasicCancel(consumerTag);
                };

                while (true) { }
            }
        }

        static void ProcessarMatricula(string message, IModel channel, ulong deliveryTag)
        {
            var matriculaString = message.ToString();
            int? matriculaId = null;

            try
            {
                matriculaId = Int32.Parse(matriculaString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao converter a matrícula da mensagem: {ex.Message}");
                //Se o formato tiver inválido remove da fila
                channel.BasicAck(deliveryTag, false);
                return;
            }

            Console.WriteLine("Processando matrícula com o Id: {0}", matriculaId);

            string token = GetTokenApi().Result;

            if (token == null)
            {
                Console.WriteLine("Erro ao obter o token. Mantendo na fila para reprocessamento.");
                channel.BasicNack(deliveryTag, false, true);
                return;
            }

            //var apiBaseUrl = "http://localhost:5100/api/Certificado";
            var apiBaseUrl = "http://service-api:5100/api/Certificado";
            var gerarCertificadoUrl = $"{apiBaseUrl}/GerarCertificado/{matriculaId}";
            Console.WriteLine("Enviando solicitação para rota " + gerarCertificadoUrl);

            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var response = httpClient.PostAsync(gerarCertificadoUrl, null).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Matrícula processada com sucesso! Removendo da fila.");
                        channel.BasicAck(deliveryTag, false); // Se deu certo já pode remover da fila
                    }
                    else
                    {
                        Console.WriteLine("Falha ao processar a matrícula. Mantendo na fila para reprocessamento.");
                        channel.BasicNack(deliveryTag, false, true); //Reposiciona na fila pra tentar de novo depois
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar a matrícula: {ex.Message}");
                    channel.BasicNack(deliveryTag, false, true);  //Reposiciona na fila pra tentar de novo depois
                }
            }
        }

        static async Task<string> GetTokenApi()
        {
            try
            {
                var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5200/connect/token");
                var request = new HttpRequestMessage(HttpMethod.Post, "http://service-identity:5200/connect/token");
                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                collection.Add(new KeyValuePair<string, string>("client_id", "clientConsumerConsole"));
                collection.Add(new KeyValuePair<string, string>("client_secret", "TfulATErsoDIRTiNeaTrecOUstasTORM"));
                collection.Add(new KeyValuePair<string, string>("scope", "api1"));
                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();

                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                string token = jsonResponse.access_token;
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao solicitar token: {ex.Message}");
                return null;
            }
        }
    }
}
