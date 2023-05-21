using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.ApiResponses.UserApiResponse;
using AtSepete.UI.Areas.Admin.Models.AdminVMs;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Net;

namespace AtSepete.UI.Areas.Admin.Controllers
{
    //adminin kendiyle ilgili işlemlerini yönettiğimiz controller
    public class AdminController : AdminBaseController
    {
        private readonly IMapper _mapper;

        public AdminController(IToastNotification toastNotification,IMapper mapper):base(toastNotification) 
        {
            _mapper = mapper;
        }
       

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                using (HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7286/AtSepeteApi/user/GetByIdUser/{UserId}"))
                
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DetailUserResponse user = JsonConvert.DeserializeObject<DetailUserResponse>(apiResponse);
                    if (user.IsSuccess)
                    {
                    var userDetail = _mapper.Map<UserDto, AdminAdminDetailVM>(user.Data);
                        NotifySuccess(user.Message);
                    return View(userDetail);
                    }
                    else
                    {
                        NotifyError(user.Message);
                        return RedirectToAction("LogOut", "Login");
                    }
                };

            }
        }
      
    }
}
