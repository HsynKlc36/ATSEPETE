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
    public class OrderDetailManager : GenericManager<OrderDetailDto,OrderDetail>, IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailManager(IOrderDetailRepository orderDetailRepository,IGenericRepository<OrderDetail> genericRepository,IMapper mapper):base(genericRepository,mapper)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public Task<BaseResponse<bool>> AddAsync(OrderDetailDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(Guid id, OrderDetailDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(IEnumerable<OrderDetailDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
