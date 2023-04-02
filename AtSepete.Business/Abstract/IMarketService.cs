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
    public interface IMarketService
    {
        Task<IDataResult<List<MarketListDto>>> GetAllMarketAsync();
        Task<IDataResult<MarketDto>> GetByIdMarketAsync(Guid id);
        Task<IDataResult<CreateMarketDto>> AddMarketAsync(CreateMarketDto entity);
        Task<IDataResult<UpdateMarketDto>> UpdateMarketAsync(Guid id, UpdateMarketDto updateMarketDto);
        Task<IResult> HardDeleteMarketAsync(Guid id);
        Task<IResult> SoftDeleteMarketAsync(Guid id);

    }
}
