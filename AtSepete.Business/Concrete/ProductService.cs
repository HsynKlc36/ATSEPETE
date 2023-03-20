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
    public class ProductService :IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;

        }
       
    }
}
