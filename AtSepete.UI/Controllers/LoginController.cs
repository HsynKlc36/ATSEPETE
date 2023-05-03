using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.ApiResponses;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class LoginController:BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(IHttpContextAccessor httpContextAccessor)
        {
           
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"https://localhost:7286/AtSepeteApi/user/LoginSignIn", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    LoginUserResponse loginUser = JsonConvert.DeserializeObject<LoginUserResponse>(apiResponse);
                    if (loginUser.IsSuccess)
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var decodeToken = tokenHandler.ReadJwtToken(loginUser.Data.AccessToken);
                        var role = decodeToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                        return RedirectToAction("Index", "Home", new { Area = role });
                    }
                    else
                    {
                        return View(loginVM);
                    }

                };
            };
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            if (true)//api den gelecek dataresult ın mesajı başarılıysa
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
            }
            return View();
        }
    }
}
