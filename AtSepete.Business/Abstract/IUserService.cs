using AtSepete.Business.JWT;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.Data;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IUserService 
    {

        //yukarıdaki satırlar giriş ve çıkış işlemleri için kullanılır
        Task<IDataResult<List<UserListDto>>> GetAllUserAsync();//tüm user'ları getirir
        Task<IDataResult<UserDto>> FindUserByIdAsync(Guid id);// id ye göre user getirir
        Task<IDataResult<UserDto>> GetUserAsync(ClaimsPrincipal principal);// login olan kullanıcıyı getirir
        Task<IDataResult<UserDto>> FindUserByEmailAsync(string email);//maile göre user getirir
        Task<IDataResult<List<UserDto>>> FindUsersByRoleAsync(string roleName);//role gore user'ları getirir
        Task<IDataResult<CreateUserDto>> AddUserAsync(CreateUserDto entity);//user ekleme
        Task<IDataResult<UpdateUserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);//user güncelleme
 
        Task<IResult> HardDeleteUserAsync(Guid id);//veritabanından siler
        Task<IResult> SoftDeleteUserAsync(Guid id);//IsActive false' a çeker
       

    }
}
