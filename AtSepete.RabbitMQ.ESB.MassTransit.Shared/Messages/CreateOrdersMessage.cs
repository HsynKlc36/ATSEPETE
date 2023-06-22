using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.RabbitMQ.ESB.MassTransit.Shared.Messages
{
    public class CreateOrdersMessage:IMessage
    {
        //public Guid ProductId { get; set; }
        //public Guid MarketId { get; set; }
        public List<Guid> OrderId { get; set; }
        //public string CustomerAddress { get; set; }
        //public string MarketName { get; set; }
        //public string ProductName { get; set; }
        //public string ProductPhotoPath { get; set; }
        //public decimal ProductPrice { get; set; }
        //public string ProductQuantity { get; set; }
        //public string ProductTitle { get; set; }
        //public string ProductUnit { get; set; }
        //public int ProductAmount { get; set; }
        //public DateTime OrderCreatedDate { get; set; }
    }
}
