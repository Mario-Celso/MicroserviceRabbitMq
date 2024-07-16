using MicroserviceRabbitMq.Model;
using System.Text.Json;

namespace MicroserviceRabbitMq.Services
{
    public class MessageService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public MessageService(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public async Task SendMessageAsync(MessageRequest messageRequest)
        {
            var message = new
            {
                messageRequest.Message,
                messageRequest.RecipientEmail,
                messageRequest.RecipientPhone
            };

            var messageJson = JsonSerializer.Serialize(message);

            _rabbitMQClientService.SendMessage(messageRequest.Queue, messageJson);

            await Task.CompletedTask;
        }
    }
}
