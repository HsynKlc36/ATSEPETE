using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Business.Logger;
using AtSepete.Dtos.Dto.OrderDetails;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class ShopService:IShopService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IProductMarketRepository _productMarketRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ShopService( IProductRepository productRepository, IMarketRepository marketRepository,IProductMarketRepository productMarketRepository,ICategoryRepository categoryRepository,  IMapper mapper, ILoggerService loggerService)
        {
            _productRepository = productRepository;
            _marketRepository = marketRepository;
            _productMarketRepository = productMarketRepository;
            _categoryRepository = categoryRepository;
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
                            MarketId = entity.MarketId

                    };
                    var product = await _productRepository.GetByIdAsync(entity.ProductId);
                    var market = await _marketRepository.GetByIdAsync(entity.MarketId);
                    var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                
                    if (product != null && category!= null&&market!=null)
                    {
                        shopListDto.CategoryId = category.Id;
                        shopListDto.CategoryName=category.Name;
                        shopListDto.ProductName = product.ProductName;
                        shopListDto.ProductTitle = product.Title;
                        shopListDto.ProductUnit=product.Unit;
                        shopListDto.ProductQuantity=product.Quantity;
                        shopListDto.ProductPhotoPath = product.PhotoPath;
                        shopListDto.MarketName = market.MarketName;
                    }
                   
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
    }
}
