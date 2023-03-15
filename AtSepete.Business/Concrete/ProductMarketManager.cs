using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class ProductMarketManager : GenericManager<ProductMarketDto, ProductMarket>, IProductMarketService
    {
        private readonly IProductMarketRepository _productMarketRepository;


        public ProductMarketManager(IProductMarketRepository productMarketRepository,IGenericRepository<ProductMarket> genericRepository,IMapper mapper):base(genericRepository,mapper)
        {
            _productMarketRepository = productMarketRepository;

        }
        public Task<BaseResponse<bool>> AddAsync(ProductMarketDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(Guid id, ProductMarketDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(IEnumerable<ProductMarketDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
