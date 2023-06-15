using AtSepete.Dtos.Dto.Carts;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface ICartService
    {
       Task<IResult> AddOrderAndOrderDetailAsync(List<CreateShoppingCartDto> orderList);
    }
}
