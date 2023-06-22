using AtSepete.RabbitMQ.ESB.MassTransit.Shared.Messages;
using MassTransit;

namespace AtSepete.UI.AdminConsumers
{
    public class AdminMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            if (context.Message.OrderId != null)
            {

                foreach (var item in context.Message.OrderId)
                {

                    var ordersMessage = $"Gelen Mesaj : {item.ToString()}";
                }
            }
            return Task.CompletedTask;
        }
    }
}
