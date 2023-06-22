using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.RabbitMQ.ESB.MassTransit.Shared.Messages;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class SendOrderMessageService : BackgroundService, ISendOrderMessageService
    {
        private readonly ISendEndpointProvider _sendEndPointProvider;
        private List<Guid> _orderIds;
        private bool _triggered;

        public SendOrderMessageService(ISendEndpointProvider sendEndPointProvider)
        {
            _sendEndPointProvider = sendEndPointProvider;
            _triggered = false;
        }
        public async Task GetOrders(CancellationToken stoppingToken, List<Guid> orderIds)
        {
            _orderIds = orderIds; // orderIds değerini saklıyoruz
            _triggered = true;
            await ExecuteAsync(stoppingToken); // ExecuteAsync metodu çağrıldığında önceki değeri kullanıyoruz
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_triggered)
            {

                if (_orderIds != null && _orderIds.Any())
                {
                    var sendEndPoint = await _sendEndPointProvider.GetSendEndpoint(new("queue:createOrder-Queue"));

                    await sendEndPoint.Send(new CreateOrdersMessage()
                    {
                        OrderId = _orderIds
                    });

                }
                _orderIds.Clear();
                _triggered = false;
            }

        }
    }
}
