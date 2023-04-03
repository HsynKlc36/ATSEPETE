using AtSepete.Business.Abstract;
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
using System.Security.Claims;

namespace AtSepete.Business.Concrete
{
    public class ProductService :IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<ProductDto>> GetByIdProductAsync(Guid id)
        {
            var product = await _productRepository.GetByDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                return new ErrorDataResult<ProductDto>(Messages.ProductNotFound);
            }
            return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(product), Messages.ProductFoundSuccess);
           

        }
        public async Task<IDataResult<List<ProductListDto>>> GetAllProductAsync()
        {
            var tempEntity = await _productRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<Product>, List<ProductListDto>>(tempEntity);
            return new SuccessDataResult<List<ProductListDto>>(result, Messages.ListedSuccess);


        }

        public async Task<IDataResult<CreateProductDto>> AddProductAsync(CreateProductDto entity)
        {
            try
            {
                if (entity is null)
                {
                    return new ErrorDataResult<CreateProductDto>(Messages.ObjectNotValid); 
                }
                var hasCategory = await _productRepository.AnyAsync(c => c.Barcode.Trim().ToLower() == entity.Barcode.Trim().ToLower());
                if (hasCategory)
                {
                    return new ErrorDataResult<CreateProductDto>(Messages.AddFailAlreadyExists);
                }
                var product = _mapper.Map<CreateProductDto, Product>(entity);
                var result = await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();

                var createProductDto = _mapper.Map<Product, CreateProductDto>(result);
                return new SuccessDataResult<CreateProductDto>(createProductDto, Messages.AddSuccess);
            }
            catch (Exception)
            {

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
                    return new ErrorDataResult<UpdateProductDto>(Messages.ProductNotFound);
                }
                if (updateProductDto.Barcode == product.Barcode&&updateProductDto.Id==product.Id)
                {

                    var updateProduct = _mapper.Map(updateProductDto, product);
                    await _productRepository.UpdateAsync(updateProduct);
                    await _productRepository.SaveChangesAsync();
                    return new SuccessDataResult<UpdateProductDto>(_mapper.Map<Product, UpdateProductDto>(updateProduct), Messages.UpdateSuccess);
                }
                else
                {
                    return new ErrorDataResult<UpdateProductDto>(Messages.ObjectNotValid);
                }

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

            }
            catch (Exception)
            {

                return new ErrorDataResult<UpdateProductDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdActiveOrPassiveAsync(id);
            if (product is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }

            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async Task<IResult> SoftDeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
            else
            {
                product.IsActive = false;
                product.DeletedDate = DateTime.Now;
                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.DeleteSuccess);
            }
        
        }

      
    }
}
