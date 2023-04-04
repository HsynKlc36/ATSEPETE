using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Mapper.Profiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderDetail, OrderDetailListDto>();
            CreateMap<OrderDetailListDto, OrderDetail>();
            CreateMap<OrderDetail, CreateOrderDetailDto>();
            CreateMap<CreateOrderDetailDto, OrderDetail>();
            CreateMap<OrderDetail, UpdateOrderDetailDto>();
            CreateMap<UpdateOrderDetailDto, OrderDetail>();
        }
    }
}
