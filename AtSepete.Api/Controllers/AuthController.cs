using AtSepete.Api.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtSepete.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHandler _tokenHandler;

        public AuthController(ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (userName == "Nuray" && password == "Marhan")
            {
                var token = _tokenHandler.CreateAccessToken(15);

                return Ok(token);
            }
            else
                return BadRequest("Kullanıcı adı veya şifre hatalı !");
        }

    }
}
