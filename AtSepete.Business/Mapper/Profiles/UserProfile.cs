using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto,User>();
            CreateMap<UserDto,User>();
            CreateMap<User, UserDto>();
            CreateMap<ChangePasswordDto, User>()
                .ForMember(dest => dest.Password,
                config => config.MapFrom(src => src.NewPassword))
                .ForMember(dest => dest.Email,
                config => config.Ignore());  //dto daki currentPassword ün karşılığı yok ise mapping de yazmaya gerek yok.              
            CreateMap<User,ChangePasswordDto>();
        }
    }
}
