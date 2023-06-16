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
using AtSepete.Business.JWT;

namespace AtSepete.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IMarketRepository _marketRepository;
        private readonly IProductMarketRepository _productMarketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILoggerService _loggerService;

        public CartService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IMapper mapper, IEmailSender emailSender, IMarketRepository marketRepository, IProductMarketRepository productMarketRepository, IUserRepository userRepository, ILoggerService loggerService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _marketRepository = marketRepository;
            _productMarketRepository = productMarketRepository;
            _userRepository = userRepository;
            _loggerService = loggerService;
        }

        public async Task<IResult> AddOrderAndOrderDetailAsync(List<CreateShoppingCartDto> shoppingCartList)
        {
            if (shoppingCartList.IsNullOrEmpty())
            {
                _loggerService.LogWarning(LogMessages.ShoppingCart__Not_Found);
                return new ErrorResult(Messages.ShoppingCartNotFound);
            }
            var orderListDistinct = shoppingCartList.DistinctBy(x => x.MarketId).ToList();
            try
            {
                foreach (var orderDto in orderListDistinct)
                {
                    CreateOrderDto createOrderDto = new()
                    {
                        CustomerId = orderDto.CustomerId,
                        MarketId = orderDto.MarketId
                    };

                    var order = _mapper.Map<CreateOrderDto, Order>(createOrderDto);
                    var result = await _orderRepository.AddAsync(order);
                    await _orderRepository.SaveChangesAsync();

                    var orderId = result.Id;
                    var orderDetailsForMarket = shoppingCartList.Where(x => x.MarketId == orderDto.MarketId).ToList();

                    foreach (var orderDetailDto in orderDetailsForMarket)
                    {
                        CreateOrderDetailDto createOrderDetailDto = new()
                        {
                            OrderId = orderId,
                            ProductId = orderDetailDto.ProductId,
                            Amount = orderDetailDto.Quantity
                        };

                        var orderDetail = _mapper.Map<CreateOrderDetailDto, OrderDetail>(createOrderDetailDto);
                        await _orderDetailRepository.AddAsync(orderDetail);
                        await _orderDetailRepository.SaveChangesAsync();

                        var productMarket = await _productMarketRepository.GetByDefaultAsync(x => x.MarketId == orderDetailDto.MarketId && x.ProductId == orderDetailDto.ProductId);

                        if (productMarket != null && productMarket.Stock >= orderDetailDto.Quantity)
                        {
                            productMarket.Stock -= orderDetailDto.Quantity;
                            await _productMarketRepository.UpdateAsync(productMarket);
                            await _productMarketRepository.SaveChangesAsync();
                        }

                    }
                }
                var customerId = shoppingCartList.FirstOrDefault().CustomerId;
                string customerName = _userRepository.GetByIdAsync(customerId).Result.FirstName;
                string  customerEmail= _userRepository.GetByIdAsync(customerId).Result.Email;
                
                var content = $"Merhaba {customerName}, <br />" +
                    $"Siparişin başarıyla oluşturuldu! Bizi tercih ettiğin için teşekkürler  <br />" +
                    $"İyi alışverişler dileriz... <br /> <br />" +
                    $"AtSepete";
                await _emailSender.SendEmailAsync(customerEmail, "Sipariş Durumu", content);

                _loggerService.LogInfo(LogMessages.ShoppingCart_Success);
                return new SuccessResult(Messages.ShoppingCartSuccess);

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.ShoppingCart_Failed);
                return new ErrorResult(Messages.ShoppingCartFailed);
            }

        }
    }
}

