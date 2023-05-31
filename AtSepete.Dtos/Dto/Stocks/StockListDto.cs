using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Stocks
{
    public class StockListDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? MarketName { get; set; }
        public string? ProductName { get; set; }
    }
}
