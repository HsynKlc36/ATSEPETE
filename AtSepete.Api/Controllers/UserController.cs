using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Dtos.Dto.Products;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.Data;
using AtSepete.Results;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IResult = AtSepete.Results.IResult;

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _userService;

        public userController(IUserService userService)
        {
          
            _userService = userService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IDataResult<List<UserListDto>>> GetAllUser()
        {
            return await _userService.GetAllUserAsync();
        }
        [HttpGet]
        [Route("[action]/{id:Guid}")]
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
        public async Task<IDataResult<UserDto>> CheckUserSign(CheckPasswordDto checkPasswordDto)
        {
            return await _userService.CheckUserSignAsync(checkPasswordDto,true);
           
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IResult> UserChangePassword(ChangePasswordDto changePasswordDto)
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
