using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.Models;

namespace AtSepete.UI.MapperUI.Profiles
{
    public class LoginVMProfile:Profile
    {
        public LoginVMProfile()
        {
            CreateMap<ForgetPasswordVM, ForgetPasswordEmailDto>().ReverseMap();
            CreateMap<NewPasswordVM, NewPasswordDto>().ReverseMap();
            CreateMap<ChangePasswordVM, ChangePasswordDto>().ReverseMap();
        }
    }
}
