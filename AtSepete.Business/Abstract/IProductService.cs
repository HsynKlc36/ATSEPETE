using AtSepete.Entities.BaseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IProductService<ProductDto,Product>
    {
        Task<BaseResponse<ProductDto>> GetById(Guid id);
        Task<BaseResponse<ProductDto>> GetByDefault(Expression<Func<ProductDto, bool>> exp);
        Task<BaseResponse<IEnumerable<ProductDto>>> GetDefault(Expression<Func<ProductDto, bool>> exp);
        Task<BaseResponse<IEnumerable<ProductDto>>> GetAll();
        Task<BaseResponse<bool>> Add(ProductDto item);
        Task<BaseResponse<bool>> SetPassive(Guid id);
        Task<BaseResponse<bool>> SetPassive(Expression<Func<ProductDto, bool>> exp);
        Task<BaseResponse<bool>> Remove(ProductDto item);
        Task<BaseResponse<bool>> Activate(Guid id);
        Task<BaseResponse<bool>> Update(ProductDto item);
        Task<BaseResponse<bool>> Update(IEnumerable<ProductDto> items);
    }
}
