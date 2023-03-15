using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class OrderManager : GenericManager<OrderDto, Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;


        public OrderManager(IOrderRepository orderRepository, IGenericRepository<Order> genericRepository,IMapper mapper) : base(genericRepository, mapper)
        {
            _orderRepository = orderRepository;

        }
        public Task<BaseResponse<bool>> AddAsync(OrderDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(Guid id, OrderDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(IEnumerable<OrderDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
