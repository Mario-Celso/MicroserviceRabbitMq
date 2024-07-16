namespace MicroserviceRabbitMq.Model
{
    public class MessageRequest
    {
        public string Queue { get; set; }
        public string Message { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
    }
}
