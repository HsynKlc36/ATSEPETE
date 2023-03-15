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
    public interface IProductMarketService : IGenericService<ProductMarketDto, ProductMarket>
    {
        Task<BaseResponse<bool>> AddAsync(ProductMarketDto item);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, ProductMarketDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<ProductMarketDto> items);
    }
}
