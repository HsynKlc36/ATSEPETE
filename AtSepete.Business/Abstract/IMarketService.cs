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
        //Task<IDataResult<List<MarketDto>>> GetAllAsync();
        //Task<BaseResponse<Dto>> GetByIdAsync(Guid id);
        //Task<BaseResponse<bool>> SetPassiveAsync(Guid id);
        //Task<BaseResponse<bool>> RemoveAsync(Guid id);
        //Task<BaseResponse<bool>> ActivateAsync(Guid id);
        //Task<BaseResponse<bool>> AddAsync(MarketDto item);
        //Task<BaseResponse<bool>> UpdateAsync(Guid id,MarketDto item);
        //Task<BaseResponse<bool>> UpdateAsync(IEnumerable<MarketDto> items);
        //Task<BaseResponse<MarketDto>> GetByIdentityAsync(string Identity);
        //Task<BaseResponse<MarketDto>> GetByDateAsync(DateTime date);
        //Task<BaseResponse<IEnumerable<MarketDto>>> GetIdentityAsync(string Identity);
        //Task<BaseResponse<IEnumerable<MarketDto>>> GetDateAsync(DateTime date);
        //Task<BaseResponse<bool>> SetPassiveAsync(string Identity);
        //Task<BaseResponse<bool>> SetPassiveAsync(DateTime date);
    }
}
