using AtSepete.Dtos.Dto.Products;
using AtSepete.UI.Areas.Admin.Models.ProductVMs;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class ProductVMProfile:Profile
    {
        public ProductVMProfile()
        {
            CreateMap<ProductListDto,AdminProductListVM>().ReverseMap();
            CreateMap<CreateProductDto,AdminProductCreateVM>().ReverseMap();
        }
    }
}
