using AtSepete.Dtos.Dto.Categories;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Mapper.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            //DTO AND CATEGORY
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();

        }
    }
}
