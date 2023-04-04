using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Orders
{
    public class CreateOrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid MarketId { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
