using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Business.Logger;
using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class ShopService : IShopService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IProductMarketRepository _productMarketRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ShopService(IProductRepository productRepository, IMarketRepository marketRepository, IProductMarketRepository productMarketRepository, ICategoryRepository categoryRepository, IOrderDetailRepository orderDetailRepository, IMapper mapper, ILoggerService loggerService)
        {
            _productRepository = productRepository;
            _marketRepository = marketRepository;
            _productMarketRepository = productMarketRepository;
            _categoryRepository = categoryRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<IDataResult<List<ShopListDto>>> ShopListAsync()
        {
            try
            {
                var productMarkets = await _productMarketRepository.GetAllAsync();
                var result = new List<ShopListDto>();

                foreach (var entity in productMarkets)
                {
                    var shopListDto = new ShopListDto
                    {
                        ProductId = entity.ProductId,
                        ProductPrice = entity.Price,
                        ProductStock = entity.Stock,
                        MarketId = entity.MarketId,
                        CreatedDate = (DateTime)entity.CreatedDate

                    };
                    var product = await _productRepository.GetByIdAsync(entity.ProductId);
                    var market = await _marketRepository.GetByIdAsync(entity.MarketId);
                    var category = await _categoryRepository.GetByIdAsync(product.CategoryId);


                    if (product is null || category is null || market is null)
                    {
                        _loggerService.LogWarning(LogMessages.Shop_Listed_Not_Found);
                        return new ErrorDataResult<List<ShopListDto>>(Messages.ShopListedNotFound);
                    }

                    shopListDto.CategoryId = category.Id;
                    shopListDto.CategoryName = category.Name;
                    shopListDto.ProductName = product.ProductName;
                    shopListDto.ProductTitle = product.Title;
                    shopListDto.ProductUnit = product.Unit;
                    shopListDto.ProductQuantity = product.Quantity;
                    shopListDto.ProductPhotoPath = product.PhotoPath;
                    shopListDto.MarketName = market.MarketName;

                    result.Add(shopListDto);
                }
                _loggerService.LogInfo(LogMessages.Shop_Listed_Success);
                return new SuccessDataResult<List<ShopListDto>>(result, Messages.ShopListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Shop_Listed_Failed);
                return new ErrorDataResult<List<ShopListDto>>(Messages.ShopListedFailed);
            }
        }

        public async Task<IDataResult<List<ShopProductDetailDto>>> ShopProductDetailAsync(Guid productId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    _loggerService.LogWarning(LogMessages.Shop_ProductDetail_Not_Found);
                    return new ErrorDataResult<List<ShopProductDetailDto>>(Messages.ShopProductDetailNotFound);
                }
                var query = (from od in (await _productMarketRepository.GetAllAsync())
                             join m in (await _marketRepository.GetAllAsync()) on od.MarketId equals m.Id
                             join p in (await _productRepository.GetAllAsync()) on od.ProductId equals p.Id
                             join c in (await _categoryRepository.GetAllAsync()) on p.CategoryId equals c.Id
                             select new
                             {
                                 ProductId = p.Id,
                                 MarketId= od.MarketId,
                                 ProductName = p.ProductName,
                                 ProductQuantity = p.Quantity,
                                 ProductUnit = p.Unit,
                                 ProductTitle = p.Title,
                                 ProductPhotoPath = p.PhotoPath,
                                 CategoryName = c.Name,
                                 MarketName = m.MarketName,
                                 ProductPrice = od.Price,
                                 ProductStock = od.Stock,
                                 ProductDescription = p.Description
                             }).ToList();
                var shopFilter = query.Where(x => x.ProductId == productId).ToList();
                 var shopProductDetail= _mapper.Map<List<ShopProductDetailDto>>(shopFilter);
                _loggerService.LogInfo(LogMessages.Shop_ProductDetail_Success);
                return new SuccessDataResult<List<ShopProductDetailDto>>(shopProductDetail, Messages.ShopProductDetailSuccess);

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Shop_ProductDetail_Failed);
                return new ErrorDataResult<List<ShopProductDetailDto>>(Messages.ShopProductDetailFailed);
            }
           
           
        }
        public async Task<IDataResult<List<BestSellerProductListDto>>> BestSellerProductsAsync()
        {
            try
            {
                var query = (from od in (await _orderDetailRepository.GetAllAsync())
                             join p in (await _productRepository.GetAllAsync()) on od.ProductId equals p.Id
                             group new { od, p } by od.ProductId into grouped
                             let sumAmount = grouped.Sum(item => item.od.Amount)
                             orderby sumAmount descending
                             select new
                             {
                                 ProductId = grouped.Key,
                                 ProductName = grouped.FirstOrDefault().p.ProductName,
                                 ProductQuantity = grouped.FirstOrDefault().p.Quantity,
                                 ProductUnit = grouped.FirstOrDefault().p.Unit,
                                 ProductTitle = grouped.FirstOrDefault().p.Title,
                                 ProductPhotoPath = grouped.FirstOrDefault().p.PhotoPath
                             }).Take(6).ToList();

                var result = query.ToList();

                if (result is null)
                {
                    _loggerService.LogWarning(LogMessages.BestSeller_Listed_Failed);
                    return new ErrorDataResult<List<BestSellerProductListDto>>(Messages.BestSellerListedFailed);
                }
                var BestSellerProducts = _mapper.Map<List<BestSellerProductListDto>>(result);
                _loggerService.LogInfo(LogMessages.BestSeller_Listed_Success);
                return new SuccessDataResult<List<BestSellerProductListDto>>(BestSellerProducts, Messages.BestSellerListedSuccess);

            }

            catch (Exception)
            {
                _loggerService.LogError(LogMessages.BestSeller_Listed_Failed);
                return new ErrorDataResult<List<BestSellerProductListDto>>(Messages.BestSellerListedFailed);
            }
        }

        public async Task<IDataResult<List<ShopFilterListDto>>> ShopFilterListAsync(string filterName)
        {
            try
            {
                if (filterName is null)
                {
                    _loggerService.LogWarning(LogMessages.ShopFilter_Listed_Not_Found);
                    return new ErrorDataResult<List<ShopFilterListDto>>(Messages.ShopFilterListedNotFound);
                }

                var query = (from od in (await _productMarketRepository.GetAllAsync())
                             join m in (await _marketRepository.GetAllAsync()) on od.MarketId equals m.Id
                             join p in (await _productRepository.GetAllAsync()) on od.ProductId equals p.Id
                             join c in (await _categoryRepository.GetAllAsync()) on p.CategoryId equals c.Id
                             select new
                             {
                                 ProductId = p.Id,
                                 ProductName = p.ProductName,
                                 ProductQuantity = p.Quantity,
                                 ProductUnit = p.Unit,
                                 ProductTitle = p.Title,
                                 ProductPhotoPath = p.PhotoPath,
                                 CategoryName = c.Name,
                                 MarketName = m.MarketName,
                                 ProductPrice = od.Price,
                                 ProductStock = od.Stock,
                             }).ToList();

                var shopFilter = query.Where(x => x.ProductTitle.Replace(" ", "").Trim().ToLower().Contains(filterName.Replace(" ", "").Trim().ToLower()) || x.ProductName.Replace(" ", "").Trim().ToLower().Contains(filterName.Replace(" ", "").Trim().ToLower()) || x.CategoryName.Replace(" ", "").Trim().ToLower().Contains(filterName.Replace(" ", "").Trim().ToLower())).ToList();
                if (shopFilter is null)
                {
                    _loggerService.LogWarning(LogMessages.ShopFilter_Listed_Not_Found);
                    return new ErrorDataResult<List<ShopFilterListDto>>(Messages.ShopFilterListedNotFound);
                }

                var shopFilterResult = _mapper.Map<List<ShopFilterListDto>>(shopFilter);
                _loggerService.LogInfo(LogMessages.ShopFilter_Listed_Success);
                return new SuccessDataResult<List<ShopFilterListDto>>(shopFilterResult, Messages.ShopFilterListedSuccess);

            }
            catch (Exception)
            {

                _loggerService.LogError(LogMessages.ShopFilter_Listed_Failed);
                return new ErrorDataResult<List<ShopFilterListDto>>(Messages.ShopFilterListedFailed);
            }

        }

        public async Task<IDataResult<List<ShopSideBarFilterListDto>>> ShopSideBarFilterListAsync(string sideBarFilter)
        {
            try
            {
                if (sideBarFilter is null)
                {
                    _loggerService.LogWarning(LogMessages.ShopSideBarFilter_Listed_Not_Found);
                    return new ErrorDataResult<List<ShopSideBarFilterListDto>>(Messages.ShopSideBarFilterListedNotFound);
                }

                var query = (from od in (await _productMarketRepository.GetAllAsync())
                             join m in (await _marketRepository.GetAllAsync()) on od.MarketId equals m.Id
                             join p in (await _productRepository.GetAllAsync()) on od.ProductId equals p.Id
                             join c in (await _categoryRepository.GetAllAsync()) on p.CategoryId equals c.Id
                             select new
                             {
                                 ProductId = p.Id,
                                 ProductName = p.ProductName,
                                 ProductQuantity = p.Quantity,
                                 ProductUnit = p.Unit,
                                 ProductTitle = p.Title,
                                 ProductPhotoPath = p.PhotoPath,
                                 CategoryName = c.Name,
                                 MarketName = m.MarketName,
                                 ProductPrice = od.Price,
                                 ProductStock = od.Stock,
                             }).ToList();
                if (query.IsNullOrEmpty())
                {
                    _loggerService.LogWarning(LogMessages.ShopSideBarFilter_Listed_Not_Found);
                    return new ErrorDataResult<List<ShopSideBarFilterListDto>>(Messages.ShopSideBarFilterListedNotFound);
                }
                string[] sideBarFilterSplit = sideBarFilter.Split('*');
                string[] categories = sideBarFilterSplit[0].Trim().Replace(" ", "").Split(',');
                string[] markets = sideBarFilterSplit[1].Trim().Replace(" ", "").Split(',');
                string[] titles = sideBarFilterSplit[2].Trim().Replace(" ", "").Split(',');
                string[] priceRange = sideBarFilterSplit[3].Split('-');

                var filteredAsCategories = categories[0] != "" ? query.Where(item => categories.Contains(item.CategoryName.Trim().Replace(" ", "").ToUpper())).ToList() : query.ToList();
                var filteredAsMarkets = markets[0] != "" ? filteredAsCategories.Where(item => markets.Contains(item.MarketName.Trim().Replace(" ", "").ToUpper())).ToList() : filteredAsCategories.ToList();
                var filteredAsTitle = titles[0] != "" ? filteredAsMarkets.Where(item => titles.Contains(item.ProductTitle.Trim().Replace(" ", "").ToUpper())).ToList() : filteredAsMarkets.ToList();
                var filteredAsPrice = filteredAsTitle.Where(x => x.ProductPrice >= Convert.ToDecimal(priceRange[0]) && x.ProductPrice <= Convert.ToDecimal(priceRange[1])).ToList();

                var result = _mapper.Map<List<ShopSideBarFilterListDto>>(filteredAsPrice);
                _loggerService.LogInfo(LogMessages.ShopSideBarFilter_Listed_Success);
                return new SuccessDataResult<List<ShopSideBarFilterListDto>>(result, Messages.ShopSideBarFilterListedSuccess);

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.ShopSideBarFilter_Listed_Failed);
                return new ErrorDataResult<List<ShopSideBarFilterListDto>>(Messages.ShopSideBarFilterListedFailed);
            }

        }
    }
}
