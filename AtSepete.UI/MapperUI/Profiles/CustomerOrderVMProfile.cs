using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.UI.Areas.Customer.Models.CustomerOrderVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class CustomerOrderVMProfile:Profile
    {
        public CustomerOrderVMProfile()
        {
            CreateMap<CustomerOrderListDto, CustomerCustomerOrderListVM>().ReverseMap();  
        }
    }
}
