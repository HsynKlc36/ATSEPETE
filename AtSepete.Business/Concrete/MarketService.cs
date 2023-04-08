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
        private readonly IMarketRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductMarketService _productMarketService;
        private readonly ILoggerService _loggerService;

        public MarketService(IMarketRepository marketRepository, IMapper mapper, IProductMarketService productMarketService, ILoggerService loggerService)
        {
            _productRepository = marketRepository;
            _mapper = mapper;
            _productMarketService = productMarketService;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<MarketDto>> GetByIdMarketAsync(Guid id)
        {
            var market = await _productRepository.GetByDefaultAsync(x => x.Id == id);
            if (market is null)
            {
                _loggerService.LogWarning(Messages.MarketNotFound);
                return new ErrorDataResult<MarketDto>(Messages.MarketNotFound);
            }
            _loggerService.LogInfo(Messages.MarketFoundSuccess);
            return new SuccessDataResult<MarketDto>(_mapper.Map<MarketDto>(market), Messages.MarketFoundSuccess);

        }
        public async Task<IDataResult<List<MarketListDto>>> GetAllMarketAsync()
        {

            var tempEntity = await _productRepository.GetAllAsync();
            if (!tempEntity.Any())
            {
                _loggerService.LogWarning(Messages.MarketNotFound);
                return new ErrorDataResult<List<MarketListDto>>(Messages.MarketNotFound);
            }
            var result = _mapper.Map<IEnumerable<Market>, List<MarketListDto>>(tempEntity);
            _loggerService.LogInfo(Messages.ListedSuccess);
            return new SuccessDataResult<List<MarketListDto>>(result, Messages.ListedSuccess);

        }

        public async Task<IDataResult<CreateMarketDto>> AddMarketAsync(CreateMarketDto entity)
        {
            try
            {
                if (entity is null)
                {
                    _loggerService.LogWarning(Messages.ObjectNotValid);
                    return new ErrorDataResult<CreateMarketDto>(Messages.ObjectNotValid); ;
                }
                var hasMarket = await _productRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == entity.MarketName.Trim().ToLower());
                if (hasMarket)
                {
                    _loggerService.LogWarning(Messages.AddFailAlreadyExists);
                    return new ErrorDataResult<CreateMarketDto>(Messages.AddFailAlreadyExists);
                }
                Market market = _mapper.Map<CreateMarketDto, Market>(entity);
                var result = await _productRepository.AddAsync(market);
                await _productRepository.SaveChangesAsync();

                CreateMarketDto createMarketDto = _mapper.Map<Market, CreateMarketDto>(result);
                _loggerService.LogInfo(Messages.AddSuccess);
                return new SuccessDataResult<CreateMarketDto>(createMarketDto, Messages.AddSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.AddFail);
                return new ErrorDataResult<CreateMarketDto>(Messages.AddFail);
            }
        }


        public async Task<IDataResult<UpdateMarketDto>> UpdateMarketAsync(Guid id, UpdateMarketDto updateMarketDto)
        {
            try
            {
                var market = await _productRepository.GetByIdAsync(id);
                if (market is null)
                {
                    _loggerService.LogWarning(Messages.MarketNotFound);
                    return new ErrorDataResult<UpdateMarketDto>(Messages.MarketNotFound);
                }
                if (updateMarketDto.MarketName.Trim().ToLower() == market.MarketName.Trim().ToLower())
                {

                    var updateMarket = _mapper.Map(updateMarketDto, market);
                    await _productRepository.UpdateAsync(updateMarket);
                    await _productRepository.SaveChangesAsync();
                    _loggerService.LogInfo($"Update {market.MarketName}");
                    return new SuccessDataResult<UpdateMarketDto>(_mapper.Map<Market, UpdateMarketDto>(updateMarket), Messages.UpdateSuccess);
                }

                var hasMarket = await _productRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == updateMarketDto.MarketName.Trim().ToLower());

                if (hasMarket)
                {
                    _loggerService.LogWarning(Messages.AddFailAlreadyExists);
                    return new ErrorDataResult<UpdateMarketDto>(Messages.AddFailAlreadyExists);
                }
                else
                {
                    var updateMarket = _mapper.Map(updateMarketDto, market);
                    await _productRepository.UpdateAsync(updateMarket);
                    await _productRepository.SaveChangesAsync();
                    _loggerService.LogInfo($"Update {market.MarketName}");
                    return new SuccessDataResult<UpdateMarketDto>(_mapper.Map<Market, UpdateMarketDto>(updateMarket), Messages.UpdateSuccess);
                }

            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UpdateFail);
                return new ErrorDataResult<UpdateMarketDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteMarketAsync(Guid id)
        {
            try
            {
                var Market = await _productRepository.GetByIdActiveOrPassiveAsync(id);
                if (Market is null)
                {
                    _loggerService.LogWarning(Messages.MarketNotFound);
                    return new ErrorResult(Messages.MarketNotFound);
                }

                await _productRepository.DeleteAsync(Market);
                await _productRepository.SaveChangesAsync();
                _loggerService.LogInfo(Messages.DeleteSuccess);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.DeleteFail);
                return new ErrorResult(Messages.DeleteFail);
            }

        }

        public async Task<IResult> SoftDeleteMarketAsync(Guid id)
        {
            try
            {
                var Market = await _productRepository.GetByIdAsync(id);
                if (Market is null)
                {
                    _loggerService.LogWarning(Messages.MarketNotFound);
                    return new ErrorResult(Messages.MarketNotFound);
                }

                Market.IsActive = false;
                Market.DeletedDate = DateTime.Now;
                await _productRepository.UpdateAsync(Market);
                await _productRepository.SaveChangesAsync();
                _loggerService.LogInfo(Messages.DeleteSuccess);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.DeleteFail);
                return new ErrorResult(Messages.DeleteFail);
            }



        }


    }
}
