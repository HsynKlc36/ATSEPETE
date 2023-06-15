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
using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Business.Logger;
using AtSepete.Dtos.Dto.ProductMarkets;

namespace AtSepete.Business.Concrete
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, IMarketRepository marketRepository, IMapper mapper, ILoggerService loggerService)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _marketRepository = marketRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<OrderDetailDto>> GetByIdOrderDetailAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetByDefaultAsync(x => x.Id == id);
            if (orderDetail is null)
            {
                _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Found);
                return new ErrorDataResult<OrderDetailDto>(Messages.OrderDetailNotFound);
            }
            _loggerService.LogInfo(LogMessages.OrderDetail_Object_Found_Success);
            return new SuccessDataResult<OrderDetailDto>(_mapper.Map<OrderDetailDto>(orderDetail), Messages.OrderDetailFoundSuccess);

        }
        public async Task<IDataResult<OrderDetailDto>> GetOrderDetailWithNames(Guid id)
        {
            try
            {
                var productId = _orderDetailRepository.GetByIdAsync(id).Result.ProductId;
                var productName = _productRepository.GetByIdAsync(productId).Result?.GetFullName();
                var orderId = _orderDetailRepository.GetByIdAsync(id).Result.OrderId;
                var customerId = _orderRepository.GetByIdAsync(orderId).Result.CustomerId;
                var customerName = _userRepository.GetByIdAsync(customerId).Result?.GetFullName();
                var marketId = _orderRepository.GetByIdAsync(orderId).Result.MarketId;
                var marketName = _marketRepository.GetByIdAsync(marketId).Result.MarketName;
                var amount = _orderDetailRepository.GetByIdAsync(id).Result.Amount;
                var createdDate = _orderDetailRepository.GetByIdAsync(id).Result.CreatedDate;
                var ModifiedDate = _orderDetailRepository.GetByIdAsync(id).Result.ModifiedDate;

                var orderDetailDto = new OrderDetailDto
                {
                    Id = id,
                    ProductId = productId,
                    MarketName = marketName,
                    Amount = amount,
                    CreatedDate = createdDate,
                    ModifiedDate = ModifiedDate,
                    CustomerName = customerName,
                    ProductName = productName,
                    OrderId = orderId,

                };

                _loggerService.LogInfo(LogMessages.OrderDetail_Object_Found_Success);
                return new SuccessDataResult<OrderDetailDto>(orderDetailDto, Messages.OrderDetailFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogInfo(LogMessages.OrderDetail_Failed);
                return new ErrorDataResult<OrderDetailDto>(Messages.OrderDetailFailed);
            }
        }
        public async Task<IDataResult<List<OrderDetailListDto>>> GetAllOrderDetailWihtNameAsync()
        {
            try
            {
                var tempEntity = await _orderDetailRepository.GetAllAsync();

                var result = new List<OrderDetailListDto>();
                foreach (var entity in tempEntity)
                {
                    var orderDetailListDto = new OrderDetailListDto
                    {
                        Id = entity.Id,
                        ProductId = entity.ProductId,
                        Amount = entity.Amount,
                        CreatedDate = entity.CreatedDate,
                        ModifiedDate = entity.ModifiedDate,
                        OrderId = entity.OrderId,
                        IsActive = entity.IsActive,

                    };
                    var product = await _productRepository.GetByIdAsync(entity.ProductId);
                    if (product != null)
                    {
                        orderDetailListDto.ProductName = product.GetFullName();
                    }
                    var order = await _orderRepository.GetByIdAsync(entity.OrderId);
                    var userName = _userRepository.GetByIdAsync(order.CustomerId).Result.GetFullName();
                    if (userName != null)
                    {
                        orderDetailListDto.CustomerName = userName;
                    }
                    result.Add(orderDetailListDto);
                }
                _loggerService.LogInfo(LogMessages.OrderDetail_Listed_Success);
                return new SuccessDataResult<List<OrderDetailListDto>>(result, Messages.ListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.OrderDetail_Listed_Failed);
                return new ErrorDataResult<List<OrderDetailListDto>>(Messages.ListedFailed);
            }

        }
        public async Task<IDataResult<List<OrderDetailListDto>>> GetAllOrderDetailAsync()
        {
            try
            {
                var tempEntity = await _orderDetailRepository.GetAllAsync();
                if (!tempEntity.Any())
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Found);
                    return new ErrorDataResult<List<OrderDetailListDto>>(Messages.OrderDetailNotFound);
                }
                var result = _mapper.Map<IEnumerable<OrderDetail>, List<OrderDetailListDto>>(tempEntity);
                _loggerService.LogInfo(LogMessages.OrderDetail_Listed_Success);
                return new SuccessDataResult<List<OrderDetailListDto>>(result, Messages.ListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.OrderDetail_Listed_Failed);
                return new ErrorDataResult<List<OrderDetailListDto>>(Messages.ListedFailed);
            }

        }
        public async Task<IDataResult<List<OrderDetailListDto>>> GetAllByFilterOrderDetailAsync()
        {
            try
            {
                var tempEntity = await _orderDetailRepository.GetAllOrderDetailAsync();
                if (!tempEntity.Any())
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Found);
                    return new ErrorDataResult<List<OrderDetailListDto>>(Messages.OrderDetailNotFound);
                }
                var result = _mapper.Map<IEnumerable<OrderDetail>, List<OrderDetailListDto>>(tempEntity);
                _loggerService.LogInfo(LogMessages.OrderDetail_Listed_Success);
                return new SuccessDataResult<List<OrderDetailListDto>>(result, Messages.ListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.OrderDetail_Listed_Failed);
                return new ErrorDataResult<List<OrderDetailListDto>>(Messages.ListedFailed);
            }

        }
        public async Task<IDataResult<CreateOrderDetailDto>> AddOrderDetailAsync(CreateOrderDetailDto entity)
        {
            try
            {
                if (entity is null)
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Valid);
                    return new ErrorDataResult<CreateOrderDetailDto>(Messages.ObjectNotValid);
                }
                var order = await _orderRepository.GetByIdAsync(entity.OrderId);
                if (order is null)
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                    return new ErrorDataResult<CreateOrderDetailDto>(Messages.OrderNotFound);
                }
                var product = await _productRepository.GetByIdAsync(entity.ProductId);

                if (product is null)
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                    return new ErrorDataResult<CreateOrderDetailDto>(Messages.ProductNotFound);
                }
                var hasOrderDetail = await _orderDetailRepository.AnyAsync(x => x.ProductId.Equals(entity.ProductId) && x.OrderId.Equals(entity.OrderId));
                if (hasOrderDetail)
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Add_Fail_Already_Exists);
                    return new ErrorDataResult<CreateOrderDetailDto>(Messages.AddFailAlreadyExists);

                }
                var OrderDetail = _mapper.Map<CreateOrderDetailDto, OrderDetail>(entity);

                var result = await _orderDetailRepository.AddAsync(OrderDetail);
                await _orderDetailRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.OrderDetail_Added_Success);
                return new SuccessDataResult<CreateOrderDetailDto>(_mapper.Map<OrderDetail, CreateOrderDetailDto>(result), Messages.AddSuccess);
                #region Control+K+S
                //    var hasMarket = await _marketRepository.AnyAsync(market => market.Id == entity.MarketId);
                //    if (hasMarket)
                //    {
                //        return new ErrorDataResult<CreateOrderDetailDto>(Messages.AddFailAlreadyExists);
                //    }

                //    var market = _mapper.Map<Market>(entity);
                //    await _marketRepository.AddAsync(market);



                //    List<EducationSubject> educationSubjects = new();
                //    foreach (var SubjectId in educationCreateDto.SubjectId)
                //    {
                //        var educationSubject = new EducationSubject
                //        {
                //            EducationId = education.Id,
                //            SubjectId = SubjectId
                //        };
                //        educationSubjects.Add(educationSubject);
                //    }


                //    await _educationsSubjectsRepository.AddRangeAsync(educationSubjects);
                //    await _educationsSubjectsRepository.SaveChangesAsync();
                //    return new SuccessDataResult<EducationDto>(_mapper.Map<EducationDto>(education), Messages.AddSuccess);






                //    if (entity is null)
                //    {
                //        return new ErrorDataResult<CreateOrderDetailDto>(Messages.ObjectNotValid); ;
                //    }
                //    var hasCategory = await _orderDetailRepository.AnyAsync(c => c.Barcode.Trim().ToLower() == entity.Barcode.Trim().ToLower());
                //    if (hasCategory)
                //    {
                //        return new ErrorDataResult<CreateOrderDetailDto>(Messages.AddFailAlreadyExists);
                //    }
                //    var product = _mapper.Map<CreateOrderDetailDto, OrderDetail>(entity);
                //    var result = await _orderDetailRepository.AddAsync(product);
                //    await _orderDetailRepository.SaveChangesAsync();

                //    var createProductDto = _mapper.Map<OrderDetail, CreateOrderDetailDto>(result);
                //    return new SuccessDataResult<CreateOrderDetailDto>(createProductDto, Messages.AddSuccess);
                #endregion
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.OrderDetail_Added_Failed);
                return new ErrorDataResult<CreateOrderDetailDto>(Messages.AddFail);
            }

        }

       
        public async Task<IDataResult<UpdateOrderDetailDto>> UpdateOrderDetailAsync(Guid id, UpdateOrderDetailDto updateOrderDetailDto)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
                if (orderDetail is null)
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Found);
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.OrderDetailNotFound);
                }
                var order = await _orderRepository.GetByIdAsync(updateOrderDetailDto.OrderId);
                if (order is null)
                {
                    _loggerService.LogWarning(LogMessages.Order_Object_Not_Found);
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.OrderNotFound);
                }
                var product = await _productRepository.GetByIdAsync(updateOrderDetailDto.ProductId);

                if (product is null)
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.ProductNotFound);
                }

                if (orderDetail.Id != updateOrderDetailDto.Id)
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Valid);
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.ObjectNotValid);
                }
                var updateOrderDetail = _mapper.Map(updateOrderDetailDto, orderDetail);

                var result = await _orderDetailRepository.UpdateAsync(updateOrderDetail);
                await _orderDetailRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.OrderDetail_Updated_Success);
                return new SuccessDataResult<UpdateOrderDetailDto>(_mapper.Map<OrderDetail, UpdateOrderDetailDto>(result), Messages.UpdateSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.OrderDetail_Updated_Failed);
                return new ErrorDataResult<UpdateOrderDetailDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteOrderDetailAsync(Guid id)
        {
            try
            {

                var OrderDetail = await _orderDetailRepository.GetByIdActiveOrPassiveAsync(id);
                if (OrderDetail is null)
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Found);
                    return new ErrorResult(Messages.OrderDetailNotFound);
                }

                await _orderDetailRepository.DeleteAsync(OrderDetail);
                await _orderDetailRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.OrderDetail_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }

            catch (Exception)
            {
                _loggerService.LogError(LogMessages.OrderDetail_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }
        }

        public async Task<IResult> SoftDeleteOrderDetailAsync(Guid id)
        {
            try
            {
                var OrderDetail = await _orderDetailRepository.GetByIdAsync(id);
                if (OrderDetail is null)
                {
                    _loggerService.LogWarning(LogMessages.OrderDetail_Object_Not_Found);
                    return new ErrorResult(Messages.OrderDetailNotFound);
                }


                OrderDetail.IsActive = false;
                OrderDetail.DeletedDate = DateTime.Now;
                await _orderDetailRepository.UpdateAsync(OrderDetail);
                await _orderDetailRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.OrderDetail_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.OrderDetail_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }
        }




    }
}
