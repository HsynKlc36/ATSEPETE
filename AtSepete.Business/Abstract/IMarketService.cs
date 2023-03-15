using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IMarketService:IGenericService<MarketDto,Market>
    {
        Task<BaseResponse<bool>> AddAsync(MarketDto item);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, MarketDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<MarketDto> items);
    }
}
