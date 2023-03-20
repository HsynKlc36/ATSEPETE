using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
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

        public MarketService(IMarketRepository marketRepository, IMapper mapper)
        {
            _marketRepository = marketRepository;
        }

       
    }
}
