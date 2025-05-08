using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BookingService.Services
{
    public interface IRabbitMQService
    {
        void SendMessage<T>(T message, string routingKey);
    }

    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(IConfiguration configuration, ILogger<RabbitMQService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void SendMessage<T>(T message, string routingKey)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQ:Host"],
                    UserName = _configuration["RabbitMQ:Username"],
                    Password = _configuration["RabbitMQ:Password"]
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "skybooker_exchange", type: ExchangeType.Direct);

                    var json = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "skybooker_exchange",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                    _logger.LogInformation($"Sent message to RabbitMQ: {json}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message to RabbitMQ");
            }
        }
    }
}
