using AtSepete.Dtos.Dto.Products;
using AtSepete.Entities.Data;
using AtSepete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IProductService 
    {
        Task<IDataResult<List<ProductListDto>>> GetAllProductAsync();
        Task<IDataResult<ProductDto>> GetByIdProductAsync(Guid id);
        Task<IDataResult<CreateProductDto>> AddProductAsync(CreateProductDto entity);
        Task<IDataResult<UpdateProductDto>> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task<IResult> HardDeleteProductAsync(Guid id);
        Task<IResult> SoftDeleteProductAsync(Guid id);
       
    }
}
