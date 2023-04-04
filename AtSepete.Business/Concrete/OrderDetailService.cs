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

namespace AtSepete.Business.Concrete
{
    public class OrderDetailService :IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository,IOrderRepository orderRepository,IProductRepository productRepository,IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<OrderDetailDto>> GetByIdOrderDetailAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetByDefaultAsync(x => x.Id == id);
            if (orderDetail is null)
            {
                return new ErrorDataResult<OrderDetailDto>(Messages.OrderDetailNotFound);
            }
            return new SuccessDataResult<OrderDetailDto>(_mapper.Map<OrderDetailDto>(orderDetail), Messages.OrderDetailFoundSuccess);

        }
        public async Task<IDataResult<List<OrderDetailListDto>>> GetAllOrderDetailAsync()
        {
            var tempEntity = await _orderDetailRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<OrderDetail>, List<OrderDetailListDto>>(tempEntity);
            return new SuccessDataResult<List<OrderDetailListDto>>(result, Messages.ListedSuccess);
        }
        public async Task<IDataResult<CreateOrderDetailDto>> AddOrderDetailAsync(CreateOrderDetailDto entity)
        {
            try
            {
                var market = await _orderRepository.GetByIdAsync(entity.OrderId);
                if (market is null)
                {
                    return new ErrorDataResult<CreateOrderDetailDto>(Messages.OrderNotFound);
                }
                var product = await _productRepository.GetByIdAsync(entity.ProductId);

                if (product is null)
                {
                    return new ErrorDataResult<CreateOrderDetailDto>(Messages.ProductNotFound);
                }
                var hasOrderDetail = await _orderDetailRepository.AnyAsync(x => x.ProductId.Equals(entity.ProductId) && x.OrderId.Equals(entity.OrderId));
                if (hasOrderDetail)
                {
                    return new ErrorDataResult<CreateOrderDetailDto>(Messages.AddFailAlreadyExists);

                }
                var OrderDetail = _mapper.Map<CreateOrderDetailDto, OrderDetail>(entity);

                var result = await _orderDetailRepository.AddAsync(OrderDetail);
                await _orderDetailRepository.SaveChangesAsync();

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
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.OrderDetailNotFound);
                }
                var order = await _orderRepository.GetByIdAsync(updateOrderDetailDto.OrderId);
                if (order is null)
                {
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.OrderNotFound);
                }
                var product = await _productRepository.GetByIdAsync(updateOrderDetailDto.ProductId);

                if (product is null)
                {
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.ProductNotFound);
                }
               
                if (orderDetail.Id!=updateOrderDetailDto.Id)
                {
                    return new ErrorDataResult<UpdateOrderDetailDto>(Messages.ObjectNotValid);
                }
              

                var updateOrderDetail = _mapper.Map(updateOrderDetailDto, orderDetail);

                var result = await _orderDetailRepository.UpdateAsync(updateOrderDetail);
                await _orderDetailRepository.SaveChangesAsync();

                return new SuccessDataResult<UpdateOrderDetailDto>(_mapper.Map<OrderDetail, UpdateOrderDetailDto>(result), Messages.UpdateSuccess);


            }
            catch (Exception)
            {

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
                    return new ErrorResult(Messages.OrderDetailNotFound);
                }

                await _orderDetailRepository.DeleteAsync(OrderDetail);
                await _orderDetailRepository.SaveChangesAsync();

                return new SuccessResult(Messages.DeleteSuccess);
            }

            catch (Exception)
            {

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
                    return new ErrorResult(Messages.OrderDetailNotFound);
                }
                else
                {
                    OrderDetail.IsActive = false;
                    OrderDetail.DeletedDate = DateTime.Now;
                    await _orderDetailRepository.UpdateAsync(OrderDetail);
                    await _orderDetailRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.DeleteSuccess);
                }
            }
            catch (Exception)
            {

                return new ErrorResult(Messages.DeleteFail);
            }
        }

       


    }
}
