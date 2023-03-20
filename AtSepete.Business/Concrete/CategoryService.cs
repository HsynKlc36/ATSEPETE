using AtSepete.Business.Abstract;
using AtSepete.Core.CoreInterfaces;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AutoMapper;
using SendGrid;
using System.Data;
using System.Linq.Expressions;

namespace AtSepete.Business.Concrete
{
    public class CategoryService: ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<CategoryDto>> GetByDefaultAsync(Guid id)
        {
            var category = await _categoryRepository.GetByDefaultAsync(x => x.CategoryId == id);
            if (category is null)
            {
                return new ErrorDataResult<CategoryDto>("Not Found");
            }
            return new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(category), "Listeleme is Success");
        }
        public async Task<DataResult<List<CategoryDto>>> GetAllAsync()
        {
            var tempEntity = await _categoryRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<Category>, List<CategoryDto>>(tempEntity);
            return new SuccessDataResult<List<CategoryDto>>(result,"Listeleme başarılı");

            
        }
        //public async Task<BaseResponse<bool>> AddAsync(CategoryDto categoryDto)
        //{
        //    try
        //    {
        //        if (categoryDto is null)
        //        {
        //            return new BaseResponse<bool>("NoData"); ;
        //        }
        //        var tempEntity = _mapper.Map<CategoryDto, Category>(categoryDto);
        //        var result = await _repository.AddAsync(tempEntity);
        //        return new BaseResponse<bool>(result);
        //    }
        //    catch (Exception)
        //    {

        //        return new BaseResponse<bool>("Saving_Error");
        //    }
        //}

        //public async Task<BaseResponse<bool>> UpdateAsync(Guid id, CategoryDto categoryDto)
        //{
        //    try
        //    {

        //        var tempEntity = await _categoryRepository.GetByIdAsync(id);
        //        if (tempEntity is null)
        //        {
        //            return new BaseResponse<bool>("NoData");
        //        }
        //        var entity = _mapper.Map(categoryDto, tempEntity);
        //        var result = await _repository.UpdateAsync(entity);
        //        return new BaseResponse<bool>(result);
        //    }
        //    catch (Exception)
        //    {

        //        return new BaseResponse<bool>("Updating_Error");
        //    }
        //}


        //public async Task<BaseResponse<bool>> UpdateAsync(IEnumerable<CategoryDto> categoryDtos)
        //{
        //    try
        //    {

        //        foreach (var categoryDto in categoryDtos)
        //        {
        //            var tempEntity = await _categoryRepository.GetByIdAsync(categoryDto.CategoryId);

        //            if (tempEntity is null)
        //            {
        //                return new BaseResponse<bool>("NoData");
        //            }

        //            var mappedEntity = _mapper.Map(categoryDto, tempEntity);

        //            await _categoryRepository.UpdateAsync(mappedEntity);
        //        }

        //        return new BaseResponse<bool>(true);

        //    }
        //    catch (Exception)
        //    {
        //        return new BaseResponse<bool>("Updating_Error");
        //    }


        //}



        //public async Task<BaseResponse<CategoryDto>> GetByCategoryNameAsync(string Identity)
        //{
        //    var entity=_categoryRepository.GetByDefaultAsync(x=>x.Description==Identity)
        //    var result = _mapper.Map<IEnumerable<T>, IEnumerable<Dto>>(tempEntity);
        //    _categoryRepository.GetByDefaultAsync(x=>)
        //}

        //public async Task<BaseResponse<CategoryDto>> GetByCategoryDateAsync(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetIdentityAsync(string Identity)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetDateAsync(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}
        //public async Task<BaseResponse<bool>> SetPassiveAsync(string Identity)
        //{
        //    return await _repository.SetPassiveAsync(x => x.);
        //}
        //public async Task<BaseResponse<bool>> SetPassiveAsync(DateTime date)
        //{
        //    return await _repository.SetPassiveAsync(x => x.);
        //}
    }
}
