using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Carts
{
    public class CreateShoppingCartDto
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public string ProductName { get; set; }
        public string ProductPhotoPath { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductTitle { get; set; }
        public string ProductUnit { get; set; }
        public int Quantity { get; set; }
    }
}
