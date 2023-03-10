using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AutoMapper;

namespace AtSepete.Business.Concrete
{
    public class CategoryManager:GenericManager<CategoryDto, Category> ,ICategoryService
    {
        private readonly ICategoryRepository _repository;
       
        private readonly IMapper _mapper;


        public CategoryManager(ICategoryRepository repository ,IMapper mapper):base(repository,mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<BaseResponse<bool>> AddAsync(CategoryDto item)
        {
            try
            {
                if (item is null)
                {
                    return new BaseResponse<bool>("NoData"); ;
                }
                return await _repository.AddAsync(item);
            }
            catch (Exception)
            {

                throw new Exception("Ekleme işlemi sırasında hata oluştu");
            }

        }

        public async Task<BaseResponse<bool>> UpdateAsync(CategoryDto item)
        {
            if (item is null)
            {
                return false;
            }
            return await _repository.UpdateAsync(item);
        }

        public async Task<BaseResponse<bool>> UpdateAsync(IEnumerable<CategoryDto> items)
        {
            if (items is null)
            {
                return false;
            }
            return await _repository.UpdateAsync(items);
        }
    }
}
