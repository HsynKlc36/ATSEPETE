using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Markets
{
    public class UpdateMarketDto
    {
        public Guid Id { get; set; }
        public string MarketName { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}
