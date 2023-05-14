using AtSepete.Business.JWT;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<Token>> SignInAsync(UserDto user, bool IsSuccess);// giriş yapma işlemini dener
        Task<IDataResult<Token>> RefreshTokenSignInAsync(RefreshTokenLoginDto refreshTokenLoginDto);//refresh token süresi geçerliyse access token oluşturur.
        Task<IDataResult<ChangePasswordDto>> ChangePasswordAsync(ChangePasswordDto changePasswordDto);//user parolasını değiştirmek
        Task<IResult> ResetPasswordAsync(NewPasswordDto newPasswordDto);//user parolasını sıfırlamak
        Task<IResult> CheckPasswordAsync(CheckPasswordDto checkPasswordDto);//user'ın şifresini kontrol eder!
        Task<IDataResult<string>> ForgetPasswordEmailSenderAsync(ForgetPasswordEmailDto emailDto);
        Task<IDataResult<UserDto>> CheckUserSignAsync(CheckPasswordDto checkPasswordDto, bool lockoutOnFailure);//giriş yapmak isteyen kullanıcıyı database'den kontrol edecek!
        Task<IResult> UpdateRefreshToken(string refreshToken, UserDto userDto, DateTime accessTokenDate, int AddOnAccessTokenDate);//user login olunca verilecek refresh tokenı belirler.dto ya RefreshToken ve süresi parametreleri eklenmeli!!
        Task<IResult> SignOutAsync();
    }
}
