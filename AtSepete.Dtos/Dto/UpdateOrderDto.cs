using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto
{
    public  class UpdateOrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid MarketId { get; set; }
        public DateTime? ModifiedDate { get; set; }= DateTime.Now;

    }
}
