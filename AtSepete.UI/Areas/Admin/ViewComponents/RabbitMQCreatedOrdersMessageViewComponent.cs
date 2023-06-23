using AtSepete.UI.AdminConsumers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace AtSepete.UI.Areas.Admin.ViewComponents
{
    public class RabbitMQCreatedOrdersMessageViewComponent:ViewComponent
    {
        private readonly AdminMessageConsumer _adminMessageConsumer;
        private readonly IDistributedCache _distributedCache;

        public RabbitMQCreatedOrdersMessageViewComponent(AdminMessageConsumer adminMessageConsumer,IDistributedCache distributedCache)
        {
            _adminMessageConsumer = adminMessageConsumer;
            _distributedCache = distributedCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string cacheKey = "MessageList";

            byte[] messageBytes = await _distributedCache.GetAsync(cacheKey);
            if (messageBytes != null && messageBytes.Length > 0)
            {
                string latestMessage = Encoding.UTF8.GetString(messageBytes);
                List<string> messages = JsonConvert.DeserializeObject<List<string>>(latestMessage);
                return View("Default", messages);
            }

            // Önbellekte hiç mesaj yoksa boş bir mesaj dön
            return View("Default", new List<string>()); // En son mesajı al
          
        }

    }
}
