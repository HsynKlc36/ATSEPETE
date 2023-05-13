using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.Data;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Web;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class userController : ControllerBase
    {
        private readonly IUserService _userService;

        public userController(IUserService userService)
        {

            _userService = userService;
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Customer")]
        public async Task<IDataResult<List<UserListDto>>> GetAllUser()
        {
            return await _userService.GetAllUserAsync();
        }
        [HttpGet]
        [Route("[action]/{id:Guid}")]
        [Authorize(AuthenticationSchemes = "Admin,Customer")]
        public async Task<IDataResult<UserDto>> GetByIdUser(Guid id)
        {
            return await _userService.FindUserByIdAsync(id);
        }
        [HttpGet]
        [Route("[action]/{email}")]
        public async Task<IDataResult<UserDto>> GetUserByEmail(string email)
        {
            return await _userService.FindUserByEmailAsync(email);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<UserDto>> CheckUserSignIn(CheckPasswordDto checkPasswordDto)
        {
            return await _userService.CheckUserSignAsync(checkPasswordDto, true);

        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IDataResult<Token>> LoginSignIn(CheckPasswordDto checkPasswordDto)
        {
            var responseUserDto = await _userService.CheckUserSignAsync(checkPasswordDto, true);//userDto elimize ulaşır               
            return await _userService.SignInAsync(responseUserDto.Data, responseUserDto.IsSuccess);

        }
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IDataResult<Token>> RefreshTokenLoginSignIn(RefreshTokenLoginDto refreshTokenLoginDto)
        {
            return await _userService.RefreshTokenSignInAsync(refreshTokenLoginDto);
        }
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IDataResult<string>> ForgetPasswordEmailSender(ForgetPasswordEmailDto emailDto)
        {
            return await _userService.ForgetPasswordEmailSenderAsync(emailDto);

        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "ForgetPassword")]
        public async Task<IResult> ResetPassword(NewPasswordDto newPasswordDto)
        {
            return await _userService.ResetPasswordAsync(newPasswordDto);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "Admin,Customer")]
        public async Task<IResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            return await _userService.ChangePasswordAsync(changePasswordDto);

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IDataResult<CreateUserDto>> AddUser([FromBody] CreateUserDto createUserDto)
        {
            return await _userService.AddUserAsync(createUserDto);
        }
        [HttpPut]
        [Route("[action]/{id:Guid}")]
        public async Task<IDataResult<UpdateUserDto>> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            return await _userService.UpdateUserAsync(id, updateUserDto);
        }

        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> HardDeleteUser([FromRoute] Guid id)
        {
            return await _userService.HardDeleteUserAsync(id);
        }
        [HttpDelete]
        [Route("[Action]/{id:Guid}")]
        public async Task<IResult> SoftDeleteUser([FromRoute] Guid id)
        {
            return await _userService.SoftDeleteUserAsync(id);
        }

    }
}
