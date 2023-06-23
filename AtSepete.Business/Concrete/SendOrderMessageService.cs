using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.RabbitMQ.ESB.MassTransit.Shared.Messages;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
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
        private string _createdOrders;
        private bool _triggered;

        public SendOrderMessageService(ISendEndpointProvider sendEndPointProvider)
        {

            _triggered = false;
            _sendEndPointProvider = sendEndPointProvider;
        }

        public async Task GetCreatedOrders(string message)
        {           
            _createdOrders = message;
            _triggered = true;
            await TriggerExecution();
           
        }

        public async Task TriggerExecution()
        {
            if (_triggered && !string.IsNullOrEmpty(_createdOrders)) 
            {
                var sendEndPoint = await _sendEndPointProvider.GetSendEndpoint(new Uri("queue:createOrders"));
                await sendEndPoint.Send(new CreateOrdersMessage
                {
                    Text = _createdOrders
                });
                _createdOrders = null!;
                _triggered = false;
            }

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                if (_triggered && !string.IsNullOrEmpty(_createdOrders))
                {
                    await TriggerExecution();

                }

                // İşlemler tamamlandıktan sonra bekleme süresi
                await Task.Delay(TimeSpan.FromSeconds(1000), stoppingToken);
            }

        }

    }
}
