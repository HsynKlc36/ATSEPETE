using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
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
        public async Task<IDataResult<CategoryDto>> GetByIdCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByDefaultAsync(x => x.CategoryId == id);
            if (category is null)
            {
                return new ErrorDataResult<CategoryDto>(Messages.CategoryNotFound);
            }
            return new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(category), Messages.CategoryFoundSuccess);

        }
        public async Task<IDataResult<List<CategoryListDto>>> GetAllCategoryAsync()
        {
            var tempEntity = await _categoryRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<Category>, List<CategoryListDto>>(tempEntity);
            return new SuccessDataResult<List<CategoryListDto>>(result,Messages.ListedSuccess);

            
        }

        public async Task<IDataResult<CreateCategoryDto>> AddCategoryAsync(CreateCategoryDto entity)
        {
            try
            {
                if (entity is null)
                {
                    return new ErrorDataResult<CreateCategoryDto>(Messages.ObjectNotValid); 
                }
                var hasCategory = await _categoryRepository.AnyAsync(c => c.Name.Trim().ToLower() == entity.Name.Trim().ToLower() );
                if (hasCategory)
                {
                    return new ErrorDataResult<CreateCategoryDto>(Messages.AddFailAlreadyExists);
                }
                Category category = _mapper.Map<CreateCategoryDto, Category>(entity);
                var result = await _categoryRepository.AddAsync(category);
                await _categoryRepository.SaveChangesAsync();

                CreateCategoryDto createCategoryDto =_mapper.Map<Category, CreateCategoryDto>(result);
                return new SuccessDataResult<CreateCategoryDto>(createCategoryDto, Messages.AddSuccess);
            }
            catch (Exception)
            {

                return new ErrorDataResult<CreateCategoryDto>(Messages.AddFail);
            }
        }


        public async Task<IDataResult<UpdateCategoryDto>> UpdateCategoryAsync(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            try
            {

                var category = await _categoryRepository.GetByIdAsync(id);
                if (category is null)
                {
                    return new ErrorDataResult<UpdateCategoryDto>(Messages.CategoryNotFound);
                }
                var hasCategory = await _categoryRepository.AnyAsync(c => c.Name.Trim().ToLower() == updateCategoryDto.Name.Trim().ToLower() && c.Description.Trim().ToLower() == updateCategoryDto.Description.Trim().ToLower());
                //çalıştırılınca ve den sonrası silinip denenecek!

                if (hasCategory)
                {
                    return new ErrorDataResult<UpdateCategoryDto>(Messages.AddFailAlreadyExists);
                }
                var updateCategory = _mapper.Map(updateCategoryDto, category);
                await _categoryRepository.UpdateAsync(updateCategory);
                await _categoryRepository.SaveChangesAsync();
                return new SuccessDataResult<UpdateCategoryDto>(_mapper.Map<Category,UpdateCategoryDto>(updateCategory),Messages.UpdateSuccess);
            }
            catch (Exception)
            {

                return new ErrorDataResult<UpdateCategoryDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return new ErrorResult(Messages.CategoryNotFound);
            }

            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async  Task<IResult> SoftDeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return new ErrorResult(Messages.CategoryNotFound);
            }
            else if (category.IsActive==true)
            {
            category.IsActive = false;
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return new SuccessResult(Messages.DeleteSuccess);               
            }
            return new ErrorResult(Messages.DeleteFail);
        }


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
