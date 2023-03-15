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
    public interface IProductService : IGenericService<ProductDto, Product>
    {
        Task<BaseResponse<bool>> AddAsync(ProductDto item);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, ProductDto item);
        Task<BaseResponse<bool>> UpdateAsync(IEnumerable<ProductDto> items);
    }
}
