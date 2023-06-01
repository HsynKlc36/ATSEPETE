using AtSepete.Dtos.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Reports
{
    public class ReportCountDto
    {
        public int  CountUsers { get; set; }
        public int  CountMarkets { get; set; }
        public int  CountOrders { get; set; }
        public int  CountProducts { get; set; }
    }
}
