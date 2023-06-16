using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Business.Logger;
using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.CustomerOrders;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IProductMarketRepository _productMarketRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;


        public CustomerOrderService(IProductMarketRepository productMarketRepository,IMarketRepository marketRepository,IOrderDetailRepository orderDetailRepository,IOrderRepository orderRepository,IProductRepository productRepository,IUserRepository userRepository,IMapper mapper,ILoggerService loggerService)
        {
            _productMarketRepository = productMarketRepository;
            _marketRepository = marketRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<List<CustomerOrderListDto>>> CustomerOrdersAsync(Guid customerId)
        {
            try
            {
                if (  customerId == Guid.Empty)
                {
                    _loggerService.LogWarning(LogMessages.CustomerOrder_Listed_Not_Found);
                    return new ErrorDataResult<List<CustomerOrderListDto>>(Messages.CustomerOrderList_Not_Found);
                }
                var query = (from ord in (await _orderDetailRepository.GetAllAsync())
                             join or in (await _orderRepository.GetAllAsync()) on ord.OrderId equals or.Id
                             join u in (await _userRepository.GetAllAsync()) on or.CustomerId equals u.Id
                             join m in (await _marketRepository.GetAllAsync()) on or.MarketId equals m.Id
                             join pm in (await _productMarketRepository.GetAllAsync()) on m.Id equals pm.MarketId
                             join p in (await _productRepository.GetAllAsync()) on ord.ProductId equals p.Id
                             select new
                             {
                                 CustomerId = u.Id,
                                 ProductId = p.Id,
                                 MarketId = m.Id,
                                 OrderId = or.Id,
                                 MarketName = m.MarketName,
                                 ProductName = p.ProductName,
                                 ProductQuantity = p.Quantity,
                                 ProductUnit = p.Unit,
                                 ProductTitle = p.Title,
                                 ProductPhotoPath = p.PhotoPath,
                                 ProductPrice = pm.Price,
                                 ProductAmount = ord.Amount,
                                 CustomerAddress = u.Adress,
                                 OrderCreatedDate = ord.CreatedDate,
                             }).ToList();
                var customerOrders = query.Where(x => x.CustomerId == customerId).OrderBy(x => x.OrderCreatedDate).ToList();
                var myOrders = _mapper.Map<List<CustomerOrderListDto>>(customerOrders);
                _loggerService.LogWarning(LogMessages.CustomerOrder_Listed_Success);
                return new SuccessDataResult<List<CustomerOrderListDto>>(myOrders,Messages.CustomerOrderListedSuccess);
            }
            catch (Exception)
            {

                _loggerService.LogError(LogMessages.CustomerOrder_Listed_Failed);
                return new ErrorDataResult<List<CustomerOrderListDto>> (Messages.CustomerOrderListFailed);
            }

            
        }
    }
}
