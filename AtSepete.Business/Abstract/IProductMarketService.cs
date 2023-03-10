using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IProductMarketService<ProductMarketDto,ProductMarket>
    {
        Task<BaseResponse<ProductMarketDto>> GetById(Guid id);
        Task<BaseResponse<ProductMarketDto>> GetByDefault(Expression<Func<ProductMarketDto, bool>> exp);
        Task<BaseResponse<IEnumerable<ProductMarketDto>>> GetDefault(Expression<Func<ProductMarketDto, bool>> exp);
        Task<BaseResponse<IEnumerable<ProductMarketDto>>> GetAll();
        Task<BaseResponse<bool>> Add(ProductMarketDto item);
        Task<BaseResponse<bool>> SetPassive(Guid id);
        Task<BaseResponse<bool>> SetPassive(Expression<Func<ProductMarketDto, bool>> exp);
        Task<BaseResponse<bool>> Remove(ProductMarketDto item);
        Task<BaseResponse<bool>> Activate(Guid id);
        Task<BaseResponse<bool>> Update(ProductMarketDto item);
        Task<BaseResponse<bool>> Update(IEnumerable<ProductMarketDto> items);
    }
}
