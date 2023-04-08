using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.Data;
using AtSepete.Results;
using Microsoft.AspNetCore.Mvc;



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
        public async Task<IDataResult<UserDto>> GetByUser(Guid id)
        {
            return await _userService.FindUserByIdAsync(id);
        }
        //[HttpGet]
        //[Route("[action]")]
        //public async Task<IActionResult> GetAllActiveUser()
        //{
        //    return Ok(await _userService.GetDefaultAsync(x => x.IsActive == true));
        //}
        //[HttpGet]
        //[Route("[action]/{id}")]
        //public async Task<IActionResult> GetActiveUser(Guid id)
        //{
        //    return Ok(await _userService.GetByDefault(x => x.userId == id && x.IsActive == true));
        //}
        //[HttpGet]
        //[Route("[action]")]
        //public async Task<IActionResult> GetAllPassiveUser()
        //{
        //    return Ok(await _userService.GetDefaultAsync(x => x.IsActive == false));
        //}

        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> CreateUser(User user)
        //{
        //    return Ok(await _userService.AddAsync(user));
        //}
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> updateUser([FromBody] User user)
        //{
        //    User Customer = await _userService.GetByIdAsync(user.userId);
        //    return Ok(await _userService.UpdateAsync(Customer));
        //}


        //[HttpDelete]
        //[Route("[action]/{id}")]
        //public async Task<IActionResult> PassiveUser(Guid id)
        //{
        //    return Ok(await _userService.SetPassiveAsync(id));
        //}
        //[HttpDelete]
        //[Route("[action]")]
        //public async Task<IActionResult> PassiveUser()
        //{
        //    return Ok(await _userService.SetPassive(x => x.IsActive == true));
        //}
        //[HttpDelete]
        //[Route("[action]")]
        //public async Task<IActionResult> RemoveUser(User user)
        //{
        //    return Ok(await _userService.Remove(user));
        //}
    }
}
