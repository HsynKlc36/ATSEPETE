using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Login(CheckPasswordDto checkPasswordDto)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(checkPasswordDto), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"https://localhost:7286/AtSepeteApi/user/CheckUserSign", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserApiResponse user = JsonConvert.DeserializeObject<UserApiResponse>(apiResponse);
                    return View(user);
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
