using MicroserviceRabbitMq.Model;
using MicroserviceRabbitMq.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceRabbitMq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageRequest request)
        {
            await _messageService.SendMessageAsync(request);
            return Ok(new { Status = "Message sent" });
        }
    }
}
