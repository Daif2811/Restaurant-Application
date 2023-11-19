using Microsoft.AspNetCore.SignalR;

namespace Restaurant.Hubs
{
    public class RestaurantHub : Hub
    {
        public async Task UpdateTableAvailability(int tableId, bool isAvailable)
        {
            await Clients.All.SendAsync("TableAvailabilityUpdated", tableId, isAvailable);
        }

        public async Task UpdateOrderStatus(int orderId, string status)
        {
            await Clients.All.SendAsync("OrderStatusUpdated", orderId, status);
        }
    }
}

/* 
  
  
  
  private readonly IMessageRepository _messageRepository;

        public chatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        
        public async Task SendMessage( int chatId, string message)
        {
            // string currentUserId = Context.UserIdentifier;
            var currentUserId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string senderConnectionId = Context.ConnectionId;

            Message newMessage = new Message()
            {
                SenderId = currentUserId,
                Read = true,
                Show = true,
                SendDate = DateTime.Now,
                ChatId = chatId,
                Content = message
            };


            await _messageRepository.Add(newMessage);



            await Clients.All.SendAsync("ReceiveMessage",message, senderConnectionId);
        }
 
 
 
 
 
 
 */