using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IMarketService<MarketDto,Market>
    {
        Task<BaseResponse<bool>> AddAsync(MarketDto item);
        Task<BaseResponse<bool>> UpdateAsync(MarketDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<MarketDto> items);
        Task<BaseResponse<MarketDto>> GetByIdentityAsync(string Identity);
        Task<BaseResponse<MarketDto>> GetByDateAsync(DateTime date);
        Task<BaseResponse<IEnumerable<MarketDto>>> GetIdentityAsync(string Identity);
        Task<BaseResponse<IEnumerable<MarketDto>>> GetIdentityAsync(DateTime date);
        Task<BaseResponse<bool>> SetPassiveAsync(string Identity);
        Task<BaseResponse<bool>> SetPassiveAsync(DateTime date);
    }
}
