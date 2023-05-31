using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.UI.Areas.Admin.Models.OrderDetailVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class OrderDetailVMProfile:Profile
    {
        public OrderDetailVMProfile()
        {
            CreateMap<OrderDetailListDto, AdminOrderDetailListVM>().ReverseMap();
            CreateMap<OrderDetailDto, AdminOrderDetailVM>().ReverseMap();
            CreateMap<OrderDetailDto, AdminOrderDetailUpdateVM>().ReverseMap();
            CreateMap<AdminOrderDetailUpdateVM, UpdateOrderDetailDto>().ReverseMap();
        }
    }
}
