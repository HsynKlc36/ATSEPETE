using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
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
    public class OrderDetailService :IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository,IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
        }

       
    }
}
