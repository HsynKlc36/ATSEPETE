using AtSepete.Business.Abstract;
using AtSepete.Entities.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AtSepete.Api.Controllers
{
    [Route("AtSepeteApi/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<User> _UserService;

        public UserController(IGenericService<User> UserService)
        {
            _UserService = UserService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _UserService.GetAll());
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetByUser(Guid id)
        {
            return Ok(await _UserService.GetById(id));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllActiveUser()
        {
            return Ok(await _UserService.GetDefault(x => x.IsActive == true));
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetActiveUser(Guid id)
        {
            return Ok(await _UserService.GetByDefault(x => x.UserId == id && x.IsActive == true));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllPassiveUser()
        {
            return Ok(await _UserService.GetDefault(x => x.IsActive == false));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateUser(User User)
        {
            return Ok(await _UserService.Add(User));
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody]User User)
        {
            User Customer = await _UserService.GetById(User.UserId);
            return Ok(await _UserService.Update(Customer));
        }
      
      
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> PassiveUser(Guid id)
        {
            return Ok(await _UserService.SetPassive(id));
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> PassiveUser()
        {
            return Ok(await _UserService.SetPassive(x => x.IsActive == true));
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> RemoveUser(User User)
        {
            return Ok(await _UserService.Remove(User));
        }
    }
}
