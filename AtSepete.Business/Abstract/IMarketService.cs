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
        Task<BaseResponse<MarketDto>> GetById(Guid id);
        Task<BaseResponse<MarketDto>> GetByDefault(Expression<Func<MarketDto, bool>> exp);
        Task<BaseResponse<IEnumerable<MarketDto>>> GetDefault(Expression<Func<MarketDto, bool>> exp);
        Task<BaseResponse<IEnumerable<MarketDto>>> GetAll();
        Task<BaseResponse<bool>> Add(MarketDto item);
        Task<BaseResponse<bool>> SetPassive(Guid id);
        Task<BaseResponse<bool>> SetPassive(Expression<Func<MarketDto, bool>> exp);
        Task<BaseResponse<bool>> Remove(MarketDto item);
        Task<BaseResponse<bool>> Activate(Guid id);
        Task<BaseResponse<bool>> Update(MarketDto item);
        Task<BaseResponse<bool>> Update(IEnumerable<MarketDto> items);
    }
}
