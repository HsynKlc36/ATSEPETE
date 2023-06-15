using AtSepete.Dtos.Dto.Carts;
using AtSepete.Dtos.Dto.JsonObjects;
using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Entities.Data;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class CartVMProfile:Profile
    {
        public CartVMProfile()
        {
            CreateMap<JsonShoppingCartDto,CreateShoppingCartDto>().ReverseMap();
            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderDetailDto, OrderDetail>().ReverseMap();
        }
    }
}
