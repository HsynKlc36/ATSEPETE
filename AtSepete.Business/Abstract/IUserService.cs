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
        Task<IDataResult<Token>> SignInAsync(UserDto user, bool IsSuccess);// giriş yapma işlemini dener
         Task<IDataResult<Token>> RefreshTokenSignInAsync(RefreshTokenLoginDto refreshTokenLoginDto);//refresh token süresi geçerliyse access token oluşturur.
        Task<IResult> SignOutAsync();
        //yukarıdaki satırlar giriş ve çıkış işlemleri için kullanılır
        Task<IDataResult<List<UserListDto>>> GetAllUserAsync();//tüm user'ları getirir
        Task<IDataResult<UserDto>> FindUserByIdAsync(Guid id);// id ye göre user getirir
        Task<IDataResult<UserDto>> GetUserAsync(ClaimsPrincipal principal);// login olan kullanıcıyı getirir
        Task<IDataResult<UserDto>> FindUserByEmailAsync(string email);//maile göre user getirir
        Task<IDataResult<List<UserDto>>> FindUsersByRoleAsync(string roleName);//role gore user'ları getirir
        Task<IDataResult<CreateUserDto>> AddUserAsync(CreateUserDto entity);//user ekleme
        Task<IDataResult<UpdateUserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);//user güncelleme
        Task<IDataResult<ChangePasswordDto>> ChangePasswordAsync(ChangePasswordDto changePasswordDto);//user parolasını değiştirmek
        Task<IResult> ResetPasswordAsync(NewPasswordDto newPasswordDto);//user parolasını sıfırlamak
        Task<IResult> HardDeleteUserAsync(Guid id);//veritabanından siler
        Task<IResult> SoftDeleteUserAsync(Guid id);//IsActive false' a çeker
        Task<IResult> CheckPasswordAsync(CheckPasswordDto checkPasswordDto);//user'ın şifresini kontrol eder!
        Task<IDataResult<string>> ForgetPasswordEmailSenderAsync(ForgetPasswordEmailDto emailDto);
        Task<IDataResult<UserDto>> CheckUserSignAsync(CheckPasswordDto checkPasswordDto,bool lockoutOnFailure);//giriş yapmak isteyen kullanıcıyı database'den kontrol edecek!
        Task<IResult> UpdateRefreshToken(string refreshToken, UserDto userDto, DateTime accessTokenDate, int AddOnAccessTokenDate);//user login olunca verilecek refresh tokenı belirler.dto ya RefreshToken ve süresi parametreleri eklenmeli!!


        //public virtual Task<IdentityResult> AddClaimAsync(TUser user, Claim claim);

    }
}
