﻿using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Business.Logger;
using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class ProductMarketService : IProductMarketService
    {
        private readonly IProductMarketRepository _productMarketRepository;
        private readonly IMapper _mapper;
        private readonly IMarketRepository _marketRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILoggerService _loggerService;

        public ProductMarketService(IProductMarketRepository productMarketRepository, IMapper mapper, IMarketRepository marketRepository, IProductRepository productRepository, ILoggerService loggerService)
        {
            _productMarketRepository = productMarketRepository;
            _mapper = mapper;
            _marketRepository = marketRepository;
            _productRepository = productRepository;
            _loggerService = loggerService;
        }

        public async Task<IDataResult<ProductMarketDto>> GetByIdProductMarketAsync(Guid id)
        {
            var productMarket = await _productMarketRepository.GetByDefaultAsync(x => x.Id == id);
            if (productMarket is null)
            {
                _loggerService.LogWarning(LogMessages.ProductMarket_Object_Not_Found);
                return new ErrorDataResult<ProductMarketDto>(Messages.ProductMarketNotFound);
            }

            _loggerService.LogInfo(LogMessages.ProductMarket_Object_Found_Success);
            return new SuccessDataResult<ProductMarketDto>(_mapper.Map<ProductMarketDto>(productMarket), Messages.ProductFoundSuccess);

        }
        public async Task<IDataResult<List<ProductMarketListDto>>> GetAllProductMarketAsync()
        {
            try
            {
                var tempEntity = await _productMarketRepository.GetAllAsync();
                if (!tempEntity.Any())
                {
                    _loggerService.LogWarning(LogMessages.ProductMarket_Object_Not_Found);
                    return new ErrorDataResult<List<ProductMarketListDto>>(Messages.ProductMarketNotFound);
                }

                var result = _mapper.Map<IEnumerable<ProductMarket>, List<ProductMarketListDto>>(tempEntity);
                _loggerService.LogInfo(LogMessages.ProductMarket_Listed_Success);
                return new SuccessDataResult<List<ProductMarketListDto>>(result, Messages.ListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogInfo(LogMessages.ProductMarket_Listed_Failed);
                return new ErrorDataResult<List<ProductMarketListDto>>(Messages.ListedFailed);
            }

        }
        public async Task<IDataResult<List<ProductMarketListDto>>> GetProductMarketListWithNames()
        {
            try
            {
                var tempEntity = await _productMarketRepository.GetAllAsync();
                var result = new List<ProductMarketListDto>();
                foreach (var entity in tempEntity)
                {
                    var productMarketDto = new ProductMarketListDto
                    {
                        Id = entity.Id,
                        ProductId = entity.ProductId,
                        MarketId = entity.MarketId,
                        Stock = entity.Stock,
                        Price = entity.Price,
                    };
                    var product = await _productRepository.GetByIdAsync(entity.ProductId);
                    if (product != null)
                    {
                        productMarketDto.ProductName = product.GetFullName();
                    }
                    var market = await _marketRepository.GetByIdAsync(entity.MarketId);
                    if (market != null)
                    {
                        productMarketDto.MarketName = market.MarketName;
                    }
                    result.Add(productMarketDto);
                }

                _loggerService.LogInfo(LogMessages.ProductMarket_Listed_Success);
                return new SuccessDataResult<List<ProductMarketListDto>>(result, Messages.ListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogInfo(LogMessages.ProductMarket_Listed_Failed);
                return new ErrorDataResult<List<ProductMarketListDto>>(Messages.ListedFailed);
            }
           

        }
        public async Task<IDataResult<CreateProductMarketDto>> AddProductMarketAsync(CreateProductMarketDto entity)
        {
            try
            {

                var market = await _marketRepository.GetByIdAsync(entity.MarketId);
                if (market is null)
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                    return new ErrorDataResult<CreateProductMarketDto>(Messages.MarketNotFound);
                }
                var product = await _productRepository.GetByIdAsync(entity.ProductId);

                if (product is null)
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                    return new ErrorDataResult<CreateProductMarketDto>(Messages.ProductNotFound);
                }
                var hasProductMarket = await _productMarketRepository.AnyAsync(x => x.ProductId.Equals(entity.ProductId) && x.MarketId.Equals(entity.MarketId));
                if (hasProductMarket)
                {
                    _loggerService.LogWarning(LogMessages.ProductMarket_Add_Fail_Already_Exists);
                    return new ErrorDataResult<CreateProductMarketDto>(Messages.AddFailAlreadyExists);

                }
                var productMarket = _mapper.Map<CreateProductMarketDto, ProductMarket>(entity);

                var result = await _productMarketRepository.AddAsync(productMarket);
                await _productMarketRepository.SaveChangesAsync();

                _loggerService.LogInfo(LogMessages.ProductMarket_Added_Success);
                return new SuccessDataResult<CreateProductMarketDto>(_mapper.Map<ProductMarket, CreateProductMarketDto>(result), Messages.AddSuccess);
                #region Control+K+S
                //    var hasMarket = await _marketRepository.AnyAsync(market => market.Id == entity.MarketId);
                //    if (hasMarket)
                //    {
                //        return new ErrorDataResult<CreateProductMarketDto>(Messages.AddFailAlreadyExists);
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
                //        return new ErrorDataResult<CreateProductMarketDto>(Messages.ObjectNotValid); ;
                //    }
                //    var hasCategory = await _productMarketRepository.AnyAsync(c => c.Barcode.Trim().ToLower() == entity.Barcode.Trim().ToLower());
                //    if (hasCategory)
                //    {
                //        return new ErrorDataResult<CreateProductMarketDto>(Messages.AddFailAlreadyExists);
                //    }
                //    var product = _mapper.Map<CreateProductMarketDto, ProductMarket>(entity);
                //    var result = await _productMarketRepository.AddAsync(product);
                //    await _productMarketRepository.SaveChangesAsync();

                //    var createProductDto = _mapper.Map<ProductMarket, CreateProductMarketDto>(result);
                //    return new SuccessDataResult<CreateProductMarketDto>(createProductDto, Messages.AddSuccess);
                #endregion
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.AddFail);
                return new ErrorDataResult<CreateProductMarketDto>(Messages.AddFail);
            }

        }

        public async Task<IDataResult<UpdateProductMarketDto>> UpdateProductMarketAsync(Guid id, UpdateProductMarketDto updateProductMarketDto)
        {
            try
            {
                var productMarket = await _productMarketRepository.GetByIdAsync(id);
                if (productMarket is null)
                {
                    _loggerService.LogWarning(LogMessages.ProductMarket_Object_Not_Found);
                    return new ErrorDataResult<UpdateProductMarketDto>(Messages.ProductMarketNotFound);
                }

                if (productMarket.Id != updateProductMarketDto.Id)
                {
                    _loggerService.LogWarning(LogMessages.ProductMarket_Object_Not_Valid);
                    return new ErrorDataResult<UpdateProductMarketDto>(Messages.ObjectNotValid);
                }
                var updateProductMarket = _mapper.Map(updateProductMarketDto, productMarket);

                var result = await _productMarketRepository.UpdateAsync(updateProductMarket);
                await _productMarketRepository.SaveChangesAsync();

                _loggerService.LogInfo(LogMessages.ProductMarket_Updated_Success);
                return new SuccessDataResult<UpdateProductMarketDto>(_mapper.Map<ProductMarket, UpdateProductMarketDto>(result), Messages.UpdateSuccess);

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.ProductMarket_Updated_Failed);
                return new ErrorDataResult<UpdateProductMarketDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteProductMarketAsync(Guid id)
        {
            try
            {
                var productMarket = await _productMarketRepository.GetByIdActiveOrPassiveAsync(id);
                if (productMarket is null)
                {
                    _loggerService.LogWarning(LogMessages.ProductMarket_Object_Not_Found);
                    return new ErrorResult(Messages.ProductMarketNotFound);
                }
                await _productMarketRepository.DeleteAsync(productMarket);
                await _productMarketRepository.SaveChangesAsync();

                _loggerService.LogInfo(LogMessages.ProductMarket_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }

            catch (Exception)
            {
                _loggerService.LogError(LogMessages.ProductMarket_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }

        public async Task<IResult> SoftDeleteProductMarketAsync(Guid id)
        {
            try
            {
                var productMarket = await _productMarketRepository.GetByIdAsync(id);
                if (productMarket is null)
                {
                    _loggerService.LogWarning(LogMessages.ProductMarket_Object_Not_Found);
                    return new ErrorResult(Messages.ProductMarketNotFound);
                }
                else
                {
                    productMarket.IsActive = false;
                    productMarket.DeletedDate = DateTime.Now;
                    await _productMarketRepository.UpdateAsync(productMarket);
                    await _productMarketRepository.SaveChangesAsync();
                    _loggerService.LogInfo(LogMessages.ProductMarket_Deleted_Success);
                    return new SuccessResult(Messages.DeleteSuccess);
                }
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.ProductMarket_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }

        public async Task<IDataResult<List<ProductMarketListDto>>> GetAllPricesByProduct(Guid productId)
        {

            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null)
            {
                _loggerService.LogWarning(Messages.ProductMarketNotFound);
                return new ErrorDataResult<List<ProductMarketListDto>>(Messages.ProductMarketNotFound);
            }
            IEnumerable<ProductMarket> productList = await _productMarketRepository.Where(x => x.ProductId == productId);
            var sortedProductList = productList.AsQueryable().OrderByDescending(x => x.Price).ToList();

            var result = _mapper.Map<IEnumerable<ProductMarket>, List<ProductMarketListDto>>(sortedProductList);
            _loggerService.LogInfo(Messages.ListedSuccess);
            return new SuccessDataResult<List<ProductMarketListDto>>(result, Messages.ListedSuccess);
        }
    }
}
