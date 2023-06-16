using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface ICustomerOrderService
    {
        Task<IDataResult<List<CustomerOrderListDto>>> CustomerOrdersAsync(Guid customerId);
    }
}
