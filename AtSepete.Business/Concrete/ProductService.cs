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
using System.Security.Claims;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Business.Logger;
using AtSepete.Business.CloudinaryImageUploader;
using Microsoft.AspNetCore.Http;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Business.Concrete
{
    //fotograflar için kontroller yazılacak!!!unutma
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILoggerService loggerService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<ProductDto>> GetByIdProductAsync(Guid id)
        {
            var product = await _productRepository.GetByDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                return new ErrorDataResult<ProductDto>(Messages.ProductNotFound);
            }

            _loggerService.LogInfo(LogMessages.Product_Object_Found_Success);
            return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(product), Messages.ProductFoundSuccess);

        }
        public async Task<IDataResult<List<ProductListDto>>> GetAllProductAsync()
        {
            var tempEntity = await _productRepository.GetAllAsync();
            if (!tempEntity.Any())
            {
                _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                return new ErrorDataResult<List<ProductListDto>>(Messages.ProductNotFound);
            }

            var result = _mapper.Map<IEnumerable<Product>, List<ProductListDto>>(tempEntity);
            _loggerService.LogInfo(LogMessages.Product_Listed_Success);
            return new SuccessDataResult<List<ProductListDto>>(result, Messages.ListedSuccess);
        }

        public async Task<IDataResult<CreateProductDto>> AddProductAsync(CreateProductDto entity)
        {
            try
            {
                if (entity is null)
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Valid);
                    return new ErrorDataResult<CreateProductDto>(Messages.ObjectNotValid);
                }
                var hasProductBarcode = await _productRepository.AnyAsync(c => c.Barcode.Trim().ToLower() == entity.Barcode.Trim().ToLower());
                var hasProduct = await _productRepository.AnyAsync(x => x.ProductName.Trim().ToLower() == entity.ProductName.Trim().ToLower() && x.Unit.Trim().ToLower() == entity.Unit.Trim().ToLower() && x.Quantity.Trim().ToLower() == entity.Quantity.Trim().ToLower() && x.Title.Trim().ToLower() == entity.Title.Trim().ToLower());
                if (hasProductBarcode||hasProduct)
                {
                    _loggerService.LogWarning(LogMessages.Product_Add_Fail_Already_Exists);
                    return new ErrorDataResult<CreateProductDto>(Messages.AddFailAlreadyExists);
                }

                string photoUrl = entity.PhotoFileName == null ? entity.PhotoPath : await ImageUploaderService.SaveImageAsync(entity.PhotoFileName);
                entity.PhotoPath = photoUrl;

                var product = _mapper.Map<CreateProductDto, Product>(entity);
                var result = await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();

                var createProductDto = _mapper.Map<Product, CreateProductDto>(result);
                _loggerService.LogInfo(LogMessages.Product_Added_Success);
                return new SuccessDataResult<CreateProductDto>(createProductDto, Messages.AddSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Product_Added_Failed);
                return new ErrorDataResult<CreateProductDto>(Messages.AddFail);
            }
        }


        public async Task<IDataResult<UpdateProductDto>> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {

            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product is null)
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                    return new ErrorDataResult<UpdateProductDto>(Messages.ProductNotFound);
                }
                if (updateProductDto.Barcode == product.Barcode && updateProductDto.Id == product.Id)
                {

                    string photoUrl = updateProductDto.PhotoFileName == null ? updateProductDto.PhotoPath : await ImageUploaderService.SaveImageAsync(updateProductDto.PhotoFileName);
                    updateProductDto.PhotoPath = photoUrl;

                    var updateProduct = _mapper.Map(updateProductDto, product);
                    await _productRepository.UpdateAsync(updateProduct);
                    await _productRepository.SaveChangesAsync();

                    _loggerService.LogInfo(LogMessages.Product_Updated_Success);
                    return new SuccessDataResult<UpdateProductDto>(_mapper.Map<Product, UpdateProductDto>(updateProduct), Messages.UpdateSuccess);
                }
                else
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Valid);
                    return new ErrorDataResult<UpdateProductDto>(Messages.ObjectNotValid);
                }
                #region ekSatırlar
 //var hasProduct = await _productRepository.AnyAsync(c => c.Barcode.Trim().ToLower() == updateProductDto.Barcode.Trim().ToLower());

                //if (hasProduct)
                //{
                //    return new ErrorDataResult<UpdateProductDto>(Messages.ProductNotFound);
                //}
                //else
                //{
                //    var updateProduct = _mapper.Map(updateProductDto, product);
                //    await _productRepository.UpdateAsync(updateProduct);
                //    await _productRepository.SaveChangesAsync();
                //    return new SuccessDataResult<UpdateProductDto>(_mapper.Map<Product, UpdateProductDto>(updateProduct), Messages.UpdateSuccess);
                //}
                #endregion
               

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Product_Updated_Failed);
                return new ErrorDataResult<UpdateProductDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteProductAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdActiveOrPassiveAsync(id);
                if (product is null)
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                    return new ErrorResult(Messages.ProductNotFound);
                }

                await _productRepository.DeleteAsync(product);
                await _productRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.Product_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Product_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }

        public async Task<IResult> SoftDeleteProductAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product is null)
                {
                    _loggerService.LogWarning(LogMessages.Product_Object_Not_Found);
                    return new ErrorResult(Messages.ProductNotFound);
                }
                product.IsActive = false;
                product.DeletedDate = DateTime.Now;
                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.Product_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Product_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }


    }
}
