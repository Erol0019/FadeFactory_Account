// Controllers/PromotionsController.cs
using Azure.Messaging.ServiceBus;
using FadeFactory_Accounts.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FadeFactory_Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly string _queueName = "promotionqueue";

        public PromotionsController(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        [HttpPost]
        public async Task<IActionResult> SendPromotion([FromBody] Promotion promotion)
        {
            var sender = _serviceBusClient.CreateSender(_queueName);
            var messageBody = JsonConvert.SerializeObject(new { data = promotion });
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

            await sender.SendMessageAsync(message);

            return Ok(new { Message = "Promotion message sent to queue successfully." });
        }
    }
}
