using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.ApiResponses;
using AtSepete.UI.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class LoginController : BaseController
    {


        public LoginController()
        {

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
                        var userRole = decodeToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                        var userEmail = decodeToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                        var userName = decodeToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                        var userId = decodeToken.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;

                        var claims = new List<Claim>
                        {
                                new Claim("Token", loginUser.Data.AccessToken), // Token burada eklenir
                                new Claim(ClaimTypes.Role, userRole),
                                new Claim(ClaimTypes.Email, userEmail),
                                new Claim("UserId", userId),
                                new Claim(ClaimTypes.Name, userName)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                        return RedirectToAction("Privacy", "Home");
                    }
                    else
                    {
                        return RedirectToAction("SignUp", "Login");// eğer böyle bir kullanıcı yoksa(loginvm de gönderilecek)
                    }


                };
            };
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Privacy", "Home");
        }
    }
}
