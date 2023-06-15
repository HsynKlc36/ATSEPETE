using AtSepete.Dtos.Dto.JsonObjects;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.UI.Areas.Admin.Models.OrderVMs;
using System.Collections.Generic;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class OrderVMProfile:Profile
    {
        public OrderVMProfile()
        {
            CreateMap <OrderListDto, AdminOrderListVM >().ReverseMap();
        }
    }
}
