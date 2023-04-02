using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Results;
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
        Task<IResult> SignInAsync(UserDto user, bool isPersistent, string authenticationMethod = null);
         Task<IResult> SignInAsync(UserDto user, AuthenticationProperties authenticationProperties, string authenticationMethod = null);//İkinci parametre olan authenticationProperties özelliği, kullanıcının kimlik doğrulaması işleminin niteliklerini içeren bir nesnedir. Bu nesne, kullanıcının kimlik doğrulama işlemleri sırasında taşıyabileceği ek bilgileri içerir. Örneğin, bu nesne kullanıcının dil ayarlarını, kimlik doğrulama işlemi sırasında kullanabileceği bir token'ı veya diğer kullanıcı bilgilerini içerebilir.Son parametre olan authenticationMethod özelliği, kullanıcının kimlik doğrulama yöntemini belirler.Varsayılan olarak null'dır. Ancak, özel kimlik doğrulama yöntemleri kullanmak isterseniz, bu parametre kullanılabilir.
        Task<IResult> PasswordSignInAsync(UserDto user, string password, bool isPersistent, bool lockoutOnFailure);//kullanıcı doğrulama,3.parametre sürekli oturumu açık bırakır,4. parametre ise hatalı girişlerde kullanıcıyı kitler
        Task<IResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);// kullanıcı doğrulama,3.parametre sürekli oturumu açık bırakır,4. parametre ise hatalı girişlerde kullanıcıyı kitler
        Task<IResult> SignOutAsync();
        //yukarıdaki satırlar giriş ve çıkış işlemleri için kullanılır
        Task<IDataResult<List<UserDto>>> GetAllUserAsync();//tüm user'ları getirir
        Task<IDataResult<UserDto>> FindUserByIdAsync(Guid id);// id ye göre user getirir
        Task<UserDto> GetUserAsync(ClaimsPrincipal principal);// login olan kullanıcıyı getirir
        Task<IDataResult<UserDto>> FindUserByEmailAsync(string email);//maile göre user getirir
        Task<IDataResult<UserDto>> AddToRoleAsync(UserDto user, string role);//user ekler
        Task<IDataResult<List<UserDto>>> FindUsersByRoleAsync(string roleName);//role gore user'ları getirir
        Task<IDataResult<UserDto>> AddUserAsync(UserDto entity);//user ekleme
        Task<IDataResult<UserDto>> UpdateUserAsync(Guid id, UserDto userDto);//user güncelleme
        Task<IResult> ChangePasswordAsync(UserDto user, string currentPassword, string newPassword);//user parolasını değiştirmek
        Task<IResult> ResetPasswordAsync(UserDto user, string token, string newPassword);//user parolasını sıfırlamak
        Task<IResult> HardDeleteUserAsync(Guid id);//veritabanından siler
        Task<IResult> SoftDeleteUserAsync(Guid id);//IsActive false' a çeker
       Task<IResult> CheckPasswordAsync(UserDto user, string password);//user'ın şifresini kontrol eder!

        //public virtual Task<IdentityResult> AddClaimAsync(TUser user, Claim claim);

    }
}
