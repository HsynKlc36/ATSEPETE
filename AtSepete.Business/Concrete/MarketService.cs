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
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductMarketService _productMarketService;

        public MarketService(IMarketRepository marketRepository, IMapper mapper,IProductMarketService productMarketService)
        {
            _productRepository = marketRepository;
            _mapper = mapper;
            _productMarketService = productMarketService;
        }
        public async Task<IDataResult<MarketDto>> GetByIdMarketAsync(Guid id)
        {
            var market = await _productRepository.GetByDefaultAsync(x => x.Id == id);
            if (market is null)
            {
                return new ErrorDataResult<MarketDto>(Messages.MarketNotFound);
            }
            return new SuccessDataResult<MarketDto>(_mapper.Map<MarketDto>(market), Messages.MarketFoundSuccess);

        }
        public async Task<IDataResult<List<MarketListDto>>> GetAllMarketAsync()
        {
            var tempEntity = await _productRepository.GetAllAsync();
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
                var hasMarket = await _productRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == entity.MarketName.Trim().ToLower());
                if (hasMarket)
                {
                    return new ErrorDataResult<CreateMarketDto>(Messages.AddFailAlreadyExists);
                }
                Market market = _mapper.Map<CreateMarketDto, Market>(entity);
                var result = await _productRepository.AddAsync(market);
                await _productRepository.SaveChangesAsync();

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
                var market = await _productRepository.GetByIdAsync(id);
                if (market is null)
                {
                    return new ErrorDataResult<UpdateMarketDto>(Messages.MarketNotFound);
                }
                if (updateMarketDto.MarketName == market.MarketName)
                {

                    var updateMarket = _mapper.Map(updateMarketDto, market);
                    await _productRepository.UpdateAsync(updateMarket);
                    await _productRepository.SaveChangesAsync();
                    return new SuccessDataResult<UpdateMarketDto>(_mapper.Map<Market, UpdateMarketDto>(updateMarket), Messages.UpdateSuccess);
                }

                var hasMarket = await _productRepository.AnyAsync(c => c.MarketName.Trim().ToLower() == updateMarketDto.MarketName.Trim().ToLower());

                if (hasMarket)
                {
                    return new ErrorDataResult<UpdateMarketDto>(Messages.AddFailAlreadyExists);
                }
                else
                {
                    var updateMarket = _mapper.Map(updateMarketDto, market);
                    await _productRepository.UpdateAsync(updateMarket);
                    await _productRepository.SaveChangesAsync();
                    return new SuccessDataResult<UpdateMarketDto>(_mapper.Map<Market, UpdateMarketDto>(updateMarket), Messages.UpdateSuccess);
                }

            }
            catch (Exception)
            {

                return new ErrorDataResult<UpdateMarketDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteMarketAsync(Guid id)
        {
            var Market = await _productRepository.GetByIdActiveOrPassiveAsync(id);
            if (Market is null)
            {
                return new ErrorResult(Messages.MarketNotFound);
            }

            await _productRepository.DeleteAsync(Market);
            await _productRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async Task<IResult> SoftDeleteMarketAsync(Guid id)
        {
            var Market = await _productRepository.GetByIdAsync(id);
            if (Market is null)
            {
                return new ErrorResult(Messages.MarketNotFound);
            }
            else 
            {
                Market.IsActive = false;
                Market.DeletedDate= DateTime.Now;
                await _productRepository.UpdateAsync(Market);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.DeleteSuccess);
            }
           
        }


    }
}
