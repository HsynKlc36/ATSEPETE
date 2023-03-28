using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class ProductMarketService :IProductMarketService
    {
        private readonly IProductMarketRepository _productMarketRepository;
        private readonly IMapper _mapper;
        private readonly IMarketRepository _marketRepository;
        private readonly IProductRepository _productRepository;

        public ProductMarketService(IProductMarketRepository productMarketRepository,IMapper mapper,IMarketRepository marketRepository,IProductRepository productRepository)
        {
            _productMarketRepository = productMarketRepository;
            _mapper = mapper;
            _marketRepository = marketRepository;
            _productRepository = productRepository;
        }

        public async Task<IDataResult<ProductMarketDto>> GetByIdProductMarketAsync(Guid id)
        {
            var product = await _productMarketRepository.GetByDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                return new ErrorDataResult<ProductMarketDto>(Messages.ProductNotFound);
            }
            return new SuccessDataResult<ProductMarketDto>(_mapper.Map<ProductMarketDto>(product), Messages.ProductFoundSuccess);


        }
        public async Task<IDataResult<List<ProductMarketListDto>>> GetAllProductMarketAsync()
        {
            var tempEntity = await _productMarketRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductMarket>, List<ProductMarketListDto>>(tempEntity);
            return new SuccessDataResult<List<ProductMarketListDto>>(result, Messages.ListedSuccess);


        }

        public async Task<IDataResult<CreateProductMarketDto>> AddProductMarketAsync(CreateProductMarketDto entity)
        {
            try
            {

                var market = await _marketRepository.GetByIdAsync(entity.MarketId);
                if (market is null)
                {
                    return new ErrorDataResult<CreateProductMarketDto>(Messages.AddFailAlreadyExists);
                }

                var product = await _productRepository.GetByIdAsync(entity.ProductId);
                if (product is null)
                {
                    return new ErrorDataResult<CreateProductMarketDto>(Messages.AddFailAlreadyExists);
                }

                var productMarket = new ProductMarket
                {
                    
                    ProductId=entity.ProductId,
                    MarketId=entity.MarketId ,
                    Price=entity.Price,
                    Stock=entity.Stock
                    
                };

                await _productMarketRepository.AddAsync(productMarket);
                await _productMarketRepository.SaveChangesAsync();              

                return new SuccessDataResult<CreateProductMarketDto>(Messages.AddSuccess);











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
            }
            catch (Exception)
            {

                return new ErrorDataResult<CreateProductMarketDto>(Messages.AddFail);
            }


        }


        public async Task<IDataResult<UpdateProductMarketDto>> UpdateProductMarketAsync(Guid id, UpdateProductMarketDto updateProductDto)
        {
            try
            {
                var product = await _productMarketRepository.GetByIdAsync(id);
                if (product is null)
                {
                    return new ErrorDataResult<UpdateProductMarketDto>(Messages.ProductNotFound);
                }
                //if (updateProductDto.Barcode == product.Barcode && updateProductDto.Id == product.Id)
                //{

                //    var updateProduct = _mapper.Map(updateProductDto, product);
                //    await _productMarketRepository.UpdateAsync(updateProduct);
                //    await _productMarketRepository.SaveChangesAsync();
                //    return new SuccessDataResult<UpdateProductMarketDto>(_mapper.Map<ProductMarket, UpdateProductMarketDto>(updateProduct), Messages.UpdateSuccess);
                //}
                else
                {
                    return new ErrorDataResult<UpdateProductMarketDto>(Messages.ObjectNotValid);
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

                return new ErrorDataResult<UpdateProductMarketDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteProductMarketAsync(Guid id)
        {
            var product = await _productMarketRepository.GetByIdActiveOrPassiveAsync(id);
            if (product is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }

            await _productMarketRepository.DeleteAsync(product);
            await _productMarketRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async Task<IResult> SoftDeleteProductMarketAsync(Guid id)
        {
            var product = await _productMarketRepository.GetByIdAsync(id);
            if (product is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
            else
            {
                product.IsActive = false;
                product.DeletedDate = DateTime.Now;
                await _productMarketRepository.UpdateAsync(product);
                await _productMarketRepository.SaveChangesAsync();
                return new SuccessResult(Messages.DeleteSuccess);
            }

        }
    }
}
