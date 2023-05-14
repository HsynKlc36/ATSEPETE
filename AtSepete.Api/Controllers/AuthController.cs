using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<UserDto>> CheckUserSignIn(CheckPasswordDto checkPasswordDto)
        {
            return await _authService.CheckUserSignAsync(checkPasswordDto, true);
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IDataResult<Token>> LoginSignIn(CheckPasswordDto checkPasswordDto)
        {
            var responseUserDto = await _authService.CheckUserSignAsync(checkPasswordDto, true);//userDto elimize ulaşır               
            return await _authService.SignInAsync(responseUserDto.Data, responseUserDto.IsSuccess);

        }
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IDataResult<Token>> RefreshTokenLoginSignIn(RefreshTokenLoginDto refreshTokenLoginDto)
        {
            return await _authService.RefreshTokenSignInAsync(refreshTokenLoginDto);
        }
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IDataResult<string>> ForgetPasswordEmailSender(ForgetPasswordEmailDto emailDto)
        {
            return await _authService.ForgetPasswordEmailSenderAsync(emailDto);

        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "ForgetPassword")]
        public async Task<IResult> ResetPassword(NewPasswordDto newPasswordDto)
        {
            return await _authService.ResetPasswordAsync(newPasswordDto);
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Admin,Customer")]
        public async Task<IResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            return await _authService.ChangePasswordAsync(changePasswordDto);

        }
    }
}
