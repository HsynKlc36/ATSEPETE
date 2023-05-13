using AtSepete.Dtos.Dto.Categories;
using AtSepete.UI.ApiResponses.CategoryApiResponse;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class CategoryVMProfile:Profile
    {
        public CategoryVMProfile()
        {
            CreateMap<AddCategoryResponse, UpdateCategoryDto>().ReverseMap();
         
        }
    }
}
