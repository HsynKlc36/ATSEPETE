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
    public class MarketManager : GenericManager<MarketDto,Market>, IMarketService
    {
        private readonly IMarketRepository _marketRepository;

        public MarketManager(IMarketRepository marketRepository, IMapper mapper,IGenericRepository<Market> genericRepository):base(genericRepository, mapper)
        {
            _marketRepository = marketRepository;
        }

        public Task<BaseResponse<bool>> AddAsync(MarketDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(Guid id, MarketDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(IEnumerable<MarketDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
