using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.Results.Concrete;
using AtSepete.Results;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtSepete.Dtos.Dto.Markets;
using AtSepete.Business.Logger;
using NLog.Targets;

namespace AtSepete.Business.Concrete
{
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;
        private readonly IProductMarketService _productMarketService;
        private readonly ILoggerService _loggerService;

        public MarketService(IMarketRepository marketRepository, IMapper mapper, IProductMarketService productMarketService, ILoggerService loggerService)
        {
            _marketRepository = marketRepository;
            _mapper = mapper;
            _productMarketService = productMarketService;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<MarketDto>> GetByIdMarketAsync(Guid id)
        {
            var market = await _marketRepository.GetByDefaultAsync(x => x.Id == id);
            if (market is null)
            {
                _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                return new ErrorDataResult<MarketDto>(Messages.MarketNotFound);
            }
            _loggerService.LogInfo(LogMessages.Market_Object_Found_Success);
            return new SuccessDataResult<MarketDto>(_mapper.Map<MarketDto>(market), Messages.MarketFoundSuccess);

        }
        public async Task<IDataResult<List<MarketListDto>>> GetAllMarketAsync()
        {
            try
            {
                var tempEntity = await _marketRepository.GetAllAsync();
                if (!tempEntity.Any())
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                    return new ErrorDataResult<List<MarketListDto>>(Messages.MarketNotFound);
                }
                var result = _mapper.Map<IEnumerable<Market>, List<MarketListDto>>(tempEntity);
                _loggerService.LogInfo(LogMessages.Market_Listed_Success);
                return new SuccessDataResult<List<MarketListDto>>(result, Messages.ListedSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Market_Listed_Failed);
                return new ErrorDataResult<List<MarketListDto>>(Messages.ListedFailed);
            }


        }

        public async Task<IDataResult<CreateMarketDto>> AddMarketAsync(CreateMarketDto entity)
        {
            try
            {
                if (entity is null)
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Valid);
                    return new ErrorDataResult<CreateMarketDto>(Messages.ObjectNotValid); ;
                }
                var hasMarket = await _marketRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == entity.MarketName.Trim().ToLower());
                if (hasMarket)
                {
                    _loggerService.LogWarning(LogMessages.Market_Add_Fail_Already_Exists);
                    return new ErrorDataResult<CreateMarketDto>(Messages.AddFailAlreadyExists);
                }
                Market market = _mapper.Map<CreateMarketDto, Market>(entity);
                var result = await _marketRepository.AddAsync(market);
                await _marketRepository.SaveChangesAsync();

                CreateMarketDto createMarketDto = _mapper.Map<Market, CreateMarketDto>(result);
                _loggerService.LogInfo(LogMessages.Market_Added_Success);
                return new SuccessDataResult<CreateMarketDto>(createMarketDto, Messages.AddSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Market_Added_Failed);
                return new ErrorDataResult<CreateMarketDto>(Messages.AddFail);
            }
        }


        public async Task<IDataResult<UpdateMarketDto>> UpdateMarketAsync(Guid id, UpdateMarketDto updateMarketDto)
        {
            try
            {
                var market = await _marketRepository.GetByIdAsync(id);
                if (market is null)
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                    return new ErrorDataResult<UpdateMarketDto>(Messages.MarketNotFound);
                }
                if (updateMarketDto.MarketName.Trim().ToLower() == market.MarketName.Trim().ToLower())
                {

                    var updateMarket = _mapper.Map(updateMarketDto, market);
                    await _marketRepository.UpdateAsync(updateMarket);
                    await _marketRepository.SaveChangesAsync();
                    _loggerService.LogInfo(LogMessages.Market_Updated_Success);
                    return new SuccessDataResult<UpdateMarketDto>(_mapper.Map<Market, UpdateMarketDto>(updateMarket), Messages.UpdateSuccess);
                }

                var hasMarket = await _marketRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == updateMarketDto.MarketName.Trim().ToLower());

                if (hasMarket)
                {
                    _loggerService.LogWarning(LogMessages.Market_Add_Fail_Already_Exists);
                    return new ErrorDataResult<UpdateMarketDto>(Messages.AddFailAlreadyExists);
                }
                else
                {
                    var updateMarket = _mapper.Map(updateMarketDto, market);
                    await _marketRepository.UpdateAsync(updateMarket);
                    await _marketRepository.SaveChangesAsync();
                    _loggerService.LogInfo(LogMessages.Market_Updated_Success);
                    return new SuccessDataResult<UpdateMarketDto>(_mapper.Map<Market, UpdateMarketDto>(updateMarket), Messages.UpdateSuccess);
                }

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Market_Updated_Failed);
                return new ErrorDataResult<UpdateMarketDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteMarketAsync(Guid id)
        {
            try
            {
                var Market = await _marketRepository.GetByIdActiveOrPassiveAsync(id);
                if (Market is null)
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                    return new ErrorResult(Messages.MarketNotFound);
                }

                await _marketRepository.DeleteAsync(Market);
                await _marketRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.Market_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Market_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }

        public async Task<IResult> SoftDeleteMarketAsync(Guid id)
        {
            try
            {
                var Market = await _marketRepository.GetByIdAsync(id);
                if (Market is null)
                {
                    _loggerService.LogWarning(LogMessages.Market_Object_Not_Found);
                    return new ErrorResult(Messages.MarketNotFound);
                }

                Market.IsActive = false;
                Market.DeletedDate = DateTime.Now;
                await _marketRepository.UpdateAsync(Market);
                await _marketRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.Market_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Market_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }

    }
}
