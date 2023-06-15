using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Results.Concrete;
using AtSepete.Results;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtSepete.Business.Logger;
using AtSepete.Dtos.Dto.Carts;
using AtSepete.Repositories.Concrete;
using AtSepete.Dtos.Dto.OrderDetails;

namespace AtSepete.Business.Concrete
{
    public class CartService:ICartService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IMarketRepository _marketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILoggerService _loggerService;

        public CartService(IOrderRepository orderRepository,IOrderDetailRepository orderDetailRepository,IMapper mapper,IEmailSender emailSender,IMarketRepository marketRepository,IUserRepository userRepository,ILoggerService loggerService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _marketRepository = marketRepository;
            _userRepository = userRepository;
            _loggerService = loggerService;
        }

        public async Task<IResult> AddOrderAndOrderDetailAsync(List<CreateShoppingCartDto> shoppingCartList)
        {
            //if (shoppingCartList.IsNullOrEmpty())
            //{
            //    _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
            //    return new ErrorDataResult<List<CreateOrderDto>>(Messages.OrderNotFound);
            //}
            //var orderListDistinct = shoppingCartList.DistinctBy(x => x.MarketId).ToList();
            //List<CreateOrderDto> createOrderDtos = new List<CreateOrderDto>();
            //foreach (var orderDto in orderListDistinct)
            //{
            //    try
            //    {
            //        var market = await _marketRepository.GetByIdAsync(orderDto.MarketId);
            //        if (market is null)
            //        {
            //            _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
            //            return new ErrorDataResult<List<CreateOrderDto>>(Messages.MarketNotFound);
            //        }

            //        var customer = await _userRepository.GetByIdAsync(orderDto.CustomerId);
            //        if (customer is null)
            //        {
            //            _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
            //            return new ErrorDataResult<List<CreateOrderDto>>(Messages.UserNotFound);
            //        }

            //        var order = _mapper.Map<CreateOrderDto, Order>(orderDto);
            //        var result = await _orderRepository.AddAsync(order);
            //        await _orderRepository.SaveChangesAsync();

            //        var createdOrderDto = _mapper.Map<Order, CreateOrderDto>(result);

            //        createOrderDtos.Add(createdOrderDto);
            //        // İşlemi tamamladıktan sonra elde edilen createdOrderDto'yu kullanabilirsiniz.
            //    }
            //    catch (Exception)
            //    {
            //        _loggerService.LogError(LogMessages.Order_Added_Failed);
            //        return new ErrorDataResult<List<CreateOrderDto>>(Messages.OrderAddedFailed);
            //    }
            //}
            //_loggerService.LogInfo(LogMessages.Order_Added_Success);
            //return new SuccessDataResult<List<CreateOrderDto>>(createOrderDtos, Messages.OrderAddedSuccess);


            if (shoppingCartList.IsNullOrEmpty())
            {
                _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                return new ErrorResult(Messages.OrderNotFound);
            }
            var orderListDistinct = shoppingCartList.DistinctBy(x => x.MarketId).ToList();
            foreach (var orderDto in orderListDistinct)
            {
                try
                {
                                      
                    CreateOrderDto createOrderDto = new()
                    {
                        CustomerId = orderDto.CustomerId,
                        MarketId = orderDto.MarketId
                       
                    };

                    
                   var order= _mapper.Map<CreateOrderDto,Order>(createOrderDto);

                    var result = await _orderRepository.AddAsync(order);
                    await _orderRepository.SaveChangesAsync();

                    var orderId = result.Id;


                    CreateOrderDetailDto createOrderDetailDto = new()
                    {
                        OrderId = orderId,
                        ProductId = orderDto.ProductId,
                        Amount = orderDto.Quantity
                    };


                    var orderDetail = _mapper.Map<CreateOrderDetailDto, OrderDetail>(createOrderDetailDto);
                    await _orderDetailRepository.AddAsync(orderDetail);
                    await _orderDetailRepository.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _loggerService.LogError(ex.Message);
                    // Hata durumunda gerekli işlemleri yapabilirsiniz.
                }
            }
            return null;

        }

    }
}

