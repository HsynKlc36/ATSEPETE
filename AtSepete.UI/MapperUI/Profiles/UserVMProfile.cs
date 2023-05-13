using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.Models;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class UserVMProfile:Profile
    {
        public UserVMProfile()
        {
            CreateMap<RegisterVM, CreateUserDto>().ReverseMap();
            
        }
     
    }
}
