﻿using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Dtos.Dto;
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

namespace AtSepete.Business.Concrete
{
    public class OrderService :IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper) 
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<OrderDto>> GetByIdOrderAsync(Guid id)
        {
            var product = await _orderRepository.GetByDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                return new ErrorDataResult<OrderDto>(Messages.OrderNotFound);
            }
            return new SuccessDataResult<OrderDto>(_mapper.Map<OrderDto>(product), Messages.OrderFoundSuccess);

        }
        public async Task<IDataResult<List<OrderListDto>>> GetAllOrderAsync()
        {
            var tempEntity = await _orderRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<Order>, List<OrderListDto>>(tempEntity);
            return new SuccessDataResult<List<OrderListDto>>(result, Messages.ListedSuccess);


        }

        public async Task<IDataResult<CreateOrderDto>> AddOrderAsync(CreateOrderDto entity)
        {
            try
            {
                if (entity is null)
                {
                    return new ErrorDataResult<CreateOrderDto>(Messages.ObjectNotValid); ;
                }
                var hasOrder = await _orderRepository.AnyAsync(x =>x.Customer.Id.Equals(entity.CustomerId)&& x.Market.Id.Equals(entity.MarketId));
                if (!hasOrder)
                {
                    return new ErrorDataResult<CreateOrderDto>(Messages.AddFail);
                }
                var product = _mapper.Map<CreateOrderDto, Order>(entity);
                var result = await _orderRepository.AddAsync(product);
                await _orderRepository.SaveChangesAsync();

                var createOrderDto = _mapper.Map<Order, CreateOrderDto>(result);
                return new SuccessDataResult<CreateOrderDto>(createOrderDto, Messages.AddSuccess);
            }
            catch (Exception)
            {

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
                    return new ErrorDataResult<UpdateOrderDto>(Messages.OrderNotFound);
                }

                var hasOrder= await _orderRepository.AnyAsync(x => x.Customer.Id.Equals(updateOrderDto.CustomerId) && x.Market.Id.Equals(updateOrderDto.MarketId) && x.Id.Equals(updateOrderDto.Id));

                if (hasOrder)
                {
                    var updateOrder = _mapper.Map(updateOrderDto, order);
                    await _orderRepository.UpdateAsync(updateOrder);
                    await _orderRepository.SaveChangesAsync();
                    return new SuccessDataResult<UpdateOrderDto>(_mapper.Map<Order, UpdateOrderDto>(updateOrder), Messages.UpdateSuccess);
                }
                else
                {
                    return new ErrorDataResult<UpdateOrderDto>(Messages.ObjectNotValid);
                }

            }
            catch (Exception)
            {

                return new ErrorDataResult<UpdateOrderDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdActiveOrPassiveAsync(id);
            if (order is null)
            {
                return new ErrorResult(Messages.OrderNotFound);
            }

            await _orderRepository.DeleteAsync(order);
            await _orderRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async Task<IResult> SoftDeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order is null)
            {
                return new ErrorResult(Messages.OrderNotFound);
            }
            else
            {
                order.IsActive = false;
                order.DeletedDate = DateTime.Now;
                await _orderRepository.UpdateAsync(order);
                await _orderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.DeleteSuccess);
            }

        }
    }
}
