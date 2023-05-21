using AtSepete.Dtos.Dto.Categories;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.Areas.Admin.Models.CategoryVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class CategoryVMProfile:Profile
    {
        public CategoryVMProfile()
        {
            CreateMap<AddCategoryResponse, UpdateCategoryDto>().ReverseMap();
            CreateMap<CategoryListDto,AdminCategoryListVM>().ReverseMap();
            CreateMap<CategoryDto,AdminCategoryUpdateVM>().ReverseMap();
            CreateMap<AdminCategoryUpdateVM,UpdateCategoryDto>().ReverseMap();
            CreateMap<AdminCategoryCreateVM,CreateCategoryDto>().ReverseMap();
         
        }
    }
}
