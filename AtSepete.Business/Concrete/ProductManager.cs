using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
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
    public class ProductManager : GenericManager<ProductDto, Product>, IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductManager(IProductRepository productRepository,IGenericRepository<Product> genericRepository,IMapper mapper):base(genericRepository,mapper)
        {
            _productRepository = productRepository;

        }
        public Task<BaseResponse<bool>> AddAsync(ProductDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(Guid id, ProductDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(IEnumerable<ProductDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
