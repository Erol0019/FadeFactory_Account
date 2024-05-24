// using System;
// using System.Threading.Tasks;
// using Azure.Messaging.ServiceBus;
// using FadeFactory_Accounts.Models;
// using FadeFactory_Accounts.Services;
// using Newtonsoft.Json;

// namespace FadeFactory_Accounts.Managers
// {
//     public class PromotionManager : IPromotionService
//     {
//         private readonly ServiceBusClient _serviceBusClient;
//         private readonly string _queueName = "promotionqueue";

//         public PromotionManager(ServiceBusClient serviceBusClient)
//         {
//             _serviceBusClient = serviceBusClient ?? throw new ArgumentNullException(nameof(serviceBusClient));
//         }

//         public async Task SendPromotionAsync(Promotion promotion)
//         {
//             try
//             {
//                 var sender = _serviceBusClient.CreateSender(_queueName);
//                 var messageBody = JsonConvert.SerializeObject(new { data = promotion });
//                 var message = new ServiceBusMessage(messageBody);

//                 await sender.SendMessageAsync(message);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"An error sending: {ex.Message}");
//                 throw;
//             }
//         }
//     }
// }
