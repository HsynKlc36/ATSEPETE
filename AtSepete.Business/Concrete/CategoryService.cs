using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Business.Logger;
using AtSepete.Core.CoreInterfaces;
using AtSepete.Dtos.Dto.Categories;
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
        private readonly ILoggerService _loggerService;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper ,ILoggerService loggerService) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<CategoryDto>> GetByIdCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                _loggerService.LogWarning(Messages.CategoryNotFound);
                return new ErrorDataResult<CategoryDto>(Messages.CategoryNotFound);
            }
            _loggerService.LogInfo(Messages.CategoryFoundSuccess);
            return new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(category), Messages.CategoryFoundSuccess);

        }
        public async Task<IDataResult<List<CategoryListDto>>> GetAllCategoryAsync()
        {
          
            var tempEntity = await _categoryRepository.GetAllAsync();
            if (!tempEntity.Any())
            {
                _loggerService.LogWarning(Messages.ObjectNotFound);
                return new ErrorDataResult<List<CategoryListDto>>(Messages.ObjectNotFound);
            }
            var result = _mapper.Map<IEnumerable<Category>, List<CategoryListDto>>(tempEntity);
            _loggerService.LogInfo(Messages.ListedSuccess);
            return new SuccessDataResult<List<CategoryListDto>>(result,Messages.ListedSuccess);
  
        }

        public async Task<IDataResult<CreateCategoryDto>> AddCategoryAsync(CreateCategoryDto entity)
        {
            try
            {
                if (entity is null)
                {
                    _loggerService.LogWarning(Messages.ObjectNotValid);
                    return new ErrorDataResult<CreateCategoryDto>(Messages.ObjectNotValid); 
                }
                var hasCategory = await _categoryRepository.AnyAsync(c => c.Name.Trim().ToLower() == entity.Name.Trim().ToLower() );
                if (hasCategory)
                {

                    _loggerService.LogWarning(Messages.AddFailAlreadyExists);
                    return new ErrorDataResult<CreateCategoryDto>(Messages.AddFailAlreadyExists);
                }
                Category category = _mapper.Map<CreateCategoryDto, Category>(entity);
                var result = await _categoryRepository.AddAsync(category);
                await _categoryRepository.SaveChangesAsync();

                CreateCategoryDto createCategoryDto =_mapper.Map<Category, CreateCategoryDto>(result);
                _loggerService.LogInfo(Messages.AddSuccess);
                return new SuccessDataResult<CreateCategoryDto>(createCategoryDto, Messages.AddSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.AddFail);
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
                    _loggerService.LogWarning(Messages.CategoryNotFound);
                    return new ErrorDataResult<UpdateCategoryDto>(Messages.CategoryNotFound);
                }
                var hasCategory = await _categoryRepository.AnyAsync(c => c.Name.Trim().ToLower() == updateCategoryDto.Name.Trim().ToLower() && c.Description.Trim().ToLower() == updateCategoryDto.Description.Trim().ToLower());
                //çalıştırılınca ve den sonrası silinip denenecek!

                if (hasCategory)
                {
                    _loggerService.LogWarning(Messages.AddFailAlreadyExists);
                    return new ErrorDataResult<UpdateCategoryDto>(Messages.AddFailAlreadyExists);
                }
                var updateCategory = _mapper.Map(updateCategoryDto, category);
                await _categoryRepository.UpdateAsync(updateCategory);
                await _categoryRepository.SaveChangesAsync();
                _loggerService.LogInfo(Messages.UpdateSuccess);
                return new SuccessDataResult<UpdateCategoryDto>(_mapper.Map<Category,UpdateCategoryDto>(updateCategory),Messages.UpdateSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UpdateFail);
                return new ErrorDataResult<UpdateCategoryDto>(Messages.UpdateFail);
            }
        }

        public async Task<IResult> HardDeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdActiveOrPassiveAsync(id);
            if (category is null)
            {
                _loggerService.LogWarning(Messages.CategoryNotFound);
                return new ErrorResult(Messages.CategoryNotFound);
            }

            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.SaveChangesAsync();

            _loggerService.LogInfo(Messages.DeleteSuccess);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async  Task<IResult> SoftDeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {

                _loggerService.LogWarning(Messages.CategoryNotFound);
                return new ErrorResult(Messages.CategoryNotFound);
            }

            category.IsActive = false;
            category.DeletedDate = DateTime.Now;
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();

            _loggerService.LogInfo(Messages.DeleteSuccess);
            return new SuccessResult(Messages.DeleteSuccess);               

        }


    }
}
