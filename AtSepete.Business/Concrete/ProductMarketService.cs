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
    public class ProductMarketService :IProductMarketService
    {
        private readonly IProductMarketRepository _productMarketRepository;


        public ProductMarketService(IProductMarketRepository productMarketRepository,IMapper mapper)
        {
            _productMarketRepository = productMarketRepository;

        }
       
    }
}
