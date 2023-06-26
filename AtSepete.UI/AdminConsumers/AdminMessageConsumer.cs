using AtSepete.RabbitMQ.ESB.MassTransit.Shared.Messages;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace AtSepete.UI.AdminConsumers
{
    public class AdminMessageConsumer : IConsumer<IMessage>
    {
        private readonly IDistributedCache _distributedCache;
        public AdminMessageConsumer(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task Consume(ConsumeContext<IMessage> context)//mesaj buradan tüketilecek
        {
            if (context.Message.Text != null)
            {

                await AddMessageToCache(context.Message.Text);//cache ile mesajlar önbellekte tutulacak 1 gün

            }

        }
        public async Task AddMessageToCache(string message)
        {
            var cacheKey = "MessageList";
 
            // Önbellekteki mevcut mesajları al
            var messages = await GetMessagesFromCache();

            // Yeni mesajı mevcut mesajların sonuna ekle
            messages.Add(message);

            // Mesajı önbelleğe ekle
            await SetMessageToCache(cacheKey, messages);
        }

        public async Task<List<string>> GetMessagesFromCache()
        {
            var cacheKey = "MessageList";

            // Önbellekten mesaj listesini al
            byte[] messageBytes = await _distributedCache.GetAsync(cacheKey);
            if (messageBytes != null && messageBytes.Length > 0)
            {
                string messageString = Encoding.UTF8.GetString(messageBytes);
                List<string> messages = JsonConvert.DeserializeObject<List<string>>(messageString);
                return messages;
            }

            return new List<string>();
        }

        public async Task SetMessageToCache(string key, List<string> messages)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };

            // Mesaj listesini önbelleğe ekle
            string messageString = JsonConvert.SerializeObject(messages);
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);
            await _distributedCache.SetAsync(key, messageBytes, cacheOptions);
            
        }
    }
}
