using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.Results.Concrete;
using AtSepete.Results;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Resource;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Business.Logger;
using Castle.Core.Internal;

namespace AtSepete.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public OrderService(IOrderRepository orderRepository, IMarketRepository marketRepository, IUserRepository userRepository, IMapper mapper, ILoggerService loggerService)
        {
            _orderRepository = orderRepository;
            _marketRepository = marketRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<OrderDto>> GetByIdOrderAsync(Guid id)
        {
            var product = await _orderRepository.GetByDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                return new ErrorDataResult<OrderDto>(Messages.OrderNotFound);
            }
            _loggerService.LogInfo(LogMessages.Order_Object_Found_Success);
            return new SuccessDataResult<OrderDto>(_mapper.Map<OrderDto>(product), Messages.OrderFoundSuccess);

        }
        public async Task<IDataResult<List<OrderListDto>>> GetAllOrderAsync()
        {
            try
            {
                var tempEntity = await _orderRepository.GetAllAsync();
                if (!tempEntity.Any())
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                    return new ErrorDataResult<List<OrderListDto>>(Messages.OrderNotFound);
                }
                var result = _mapper.Map<IEnumerable<Order>, List<OrderListDto>>(tempEntity);
                _loggerService.LogInfo(LogMessages.Order_Listed_Success);
                return new SuccessDataResult<List<OrderListDto>>(result, Messages.ListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Order_Listed_Failed);
                return new ErrorDataResult<List<OrderListDto>>(Messages.ListedFailed);
            }
        }

        public async Task<IDataResult<CreateOrderDto>> AddOrderAsync(CreateOrderDto entity)
        {
            try
            {
                if (entity is null)
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Valid);
                    return new ErrorDataResult<CreateOrderDto>(Messages.ObjectNotValid); ;
                }
                var market = await _marketRepository.GetByIdAsync(entity.MarketId);
                if (market is null)
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                    return new ErrorDataResult<CreateOrderDto>(Messages.MarketNotFound);
                }
                var customer = await _userRepository.GetByIdAsync(entity.CustomerId);

                if (customer is null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<CreateOrderDto>(Messages.UserNotFound);
                }
                var product = _mapper.Map<CreateOrderDto, Order>(entity);
                var result = await _orderRepository.AddAsync(product);
                await _orderRepository.SaveChangesAsync();

                var createOrderDto = _mapper.Map<Order, CreateOrderDto>(result);
                _loggerService.LogInfo(LogMessages.Order_Added_Success);
                return new SuccessDataResult<CreateOrderDto>(createOrderDto, Messages.AddSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Order_Added_Failed);
                return new ErrorDataResult<CreateOrderDto>(Messages.AddFail);
            }
        }

        public async Task<IDataResult<UpdateOrderDto>> UpdateOrderAsync(Guid id, UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order is null)
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                    return new ErrorDataResult<UpdateOrderDto>(Messages.OrderNotFound);
                }
                //
                var customer = await _userRepository.GetByIdAsync(updateOrderDto.CustomerId);
                if (customer is null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<UpdateOrderDto>(Messages.UserNotFound);
                }
                var market = await _marketRepository.GetByIdAsync(updateOrderDto.MarketId);

                if (market is null)
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                    return new ErrorDataResult<UpdateOrderDto>(Messages.MarketNotFound);
                }

                if (order.Id != updateOrderDto.Id)
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Valid);
                    return new ErrorDataResult<UpdateOrderDto>(Messages.ObjectNotValid);
                }

                var updateOrder = _mapper.Map(updateOrderDto, order);
                await _orderRepository.UpdateAsync(updateOrder);
                await _orderRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.Order_Updated_Success);
                return new SuccessDataResult<UpdateOrderDto>(_mapper.Map<Order, UpdateOrderDto>(updateOrder), Messages.UpdateSuccess);

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Order_Updated_Failed);
                return new ErrorDataResult<UpdateOrderDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteOrderAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetByIdActiveOrPassiveAsync(id);
                if (order is null)
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                    return new ErrorResult(Messages.OrderNotFound);
                }
                await _orderRepository.DeleteAsync(order);
                await _orderRepository.SaveChangesAsync();

                _loggerService.LogInfo(LogMessages.Order_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Order_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }

        public async Task<IResult> SoftDeleteOrderAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order is null)
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                    return new ErrorResult(Messages.OrderNotFound);
                }

                order.IsActive = false;
                order.DeletedDate = DateTime.Now;
                await _orderRepository.UpdateAsync(order);
                await _orderRepository.SaveChangesAsync();

                _loggerService.LogInfo(LogMessages.Order_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Order_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }


        }
    }
}
