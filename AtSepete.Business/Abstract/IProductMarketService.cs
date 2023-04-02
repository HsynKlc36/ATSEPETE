using AtSepete.Dtos.Dto;
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
    public interface IProductMarketService 
    {
        Task<IDataResult<List<ProductMarketListDto>>> GetAllProductMarketAsync();
        Task<IDataResult<List<ProductMarketListDto>>> GetAllPricesByProduct(Guid id);
    
        Task<IDataResult<ProductMarketDto>> GetByIdProductMarketAsync(Guid id);
        Task<IDataResult<CreateProductMarketDto>> AddProductMarketAsync(CreateProductMarketDto entity);
        Task<IDataResult<UpdateProductMarketDto>> UpdateProductMarketAsync(Guid id, UpdateProductMarketDto updateProductMarketDto);
        Task<IResult> HardDeleteProductMarketAsync(Guid id);
        Task<IResult> SoftDeleteProductMarketAsync(Guid id);


       
    }
}
