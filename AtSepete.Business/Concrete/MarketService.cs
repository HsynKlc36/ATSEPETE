using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Dtos.Dto;
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

namespace AtSepete.Business.Concrete
{
    public class MarketService :IMarketService
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;

        public MarketService(IMarketRepository marketRepository, IMapper mapper)
        {
            _marketRepository = marketRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<MarketDto>> GetByIdMarketAsync(Guid id)
        {
            var market = await _marketRepository.GetByDefaultAsync(x => x.MarketId == id);
            if (market is null)
            {
                return new ErrorDataResult<MarketDto>(Messages.MarketNotFound);
            }
            return new SuccessDataResult<MarketDto>(_mapper.Map<MarketDto>(market), Messages.MarketFoundSuccess);

        }
        public async Task<IDataResult<List<MarketListDto>>> GetAllMarketAsync()
        {
            var tempEntity = await _marketRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<Market>, List<MarketListDto>>(tempEntity);
            return new SuccessDataResult<List<MarketListDto>>(result, Messages.ListedSuccess);


        }

        public async Task<IDataResult<CreateMarketDto>> AddMarketAsync(CreateMarketDto entity)
        {
            try
            {
                if (entity is null)
                {
                    return new ErrorDataResult<CreateMarketDto>(Messages.ObjectNotValid); ;
                }
                var hasCategory = await _marketRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == entity.MarketName.Trim().ToLower());
                if (hasCategory)
                {
                    return new ErrorDataResult<CreateMarketDto>(Messages.AddFailAlreadyExists);
                }
                Market market = _mapper.Map<CreateMarketDto, Market>(entity);
                var result = await _marketRepository.AddAsync(market);
                await _marketRepository.SaveChangesAsync();

                CreateMarketDto createMarketDto = _mapper.Map<Market, CreateMarketDto>(result);
                return new SuccessDataResult<CreateMarketDto>(createMarketDto, Messages.AddSuccess);
            }
            catch (Exception)
            {

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
                    return new ErrorDataResult<UpdateMarketDto>(Messages.MarketNotFound);
                }
                var hasMarket = await _marketRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == updateMarketDto.MarketName.Trim().ToLower() && c.Description.Trim().ToLower() == updateMarketDto.Description.Trim().ToLower());
                //çalıştırılınca ve den sonrası silinip denenecek!

                if (hasMarket)
                {
                    return new ErrorDataResult<UpdateMarketDto>(Messages.AddFailAlreadyExists);
                }
                var updateMarket = _mapper.Map(updateMarketDto, market);
                await _marketRepository.UpdateAsync(updateMarket);
                await _marketRepository.SaveChangesAsync();
                return new SuccessDataResult<UpdateMarketDto>(_mapper.Map<Market, UpdateMarketDto>(updateMarket), Messages.UpdateSuccess);
            }
            catch (Exception)
            {

                return new ErrorDataResult<UpdateMarketDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteMarketAsync(Guid id)
        {
            var Market = await _marketRepository.GetByIdAsync(id);
            if (Market is null)
            {
                return new ErrorResult(Messages.MarketNotFound);
            }

            await _marketRepository.DeleteAsync(Market);
            await _marketRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async Task<IResult> SoftDeleteMarketAsync(Guid id)
        {
            var Market = await _marketRepository.GetByIdAsync(id);
            if (Market is null)
            {
                return new ErrorResult(Messages.MarketNotFound);
            }
            else if (Market.IsActive == true)
            {
                Market.IsActive = false;
                await _marketRepository.UpdateAsync(Market);
                await _marketRepository.SaveChangesAsync();
                return new SuccessResult(Messages.DeleteSuccess);
            }
            return new ErrorResult(Messages.DeleteFail);
        }


    }
}
