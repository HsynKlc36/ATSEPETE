using AspNetCoreHero.ToastNotification.Abstractions;
using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class BaseController:Controller
    {

        public BaseController()
        {
            
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
                using (HttpResponseMessage response = await httpClient.PostAsync($"https://localhost:7286/AtSepeteApi/user/CheckUserSign",content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserApiResponse user = JsonConvert.DeserializeObject<UserApiResponse>(apiResponse);
                    return View(user);
                };
            };
        }
        //protected INotyfService NotyfService => HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;
        //protected IStringLocalizer<SharedModelResource> Localizer => HttpContext.RequestServices.GetService(typeof(IStringLocalizer<SharedModelResource>)) as IStringLocalizer<SharedModelResource>;// dil değişimleri için kullanıldı fakat 

        //protected void NotifySuccess(string message)
        //{
        //    NotyfService.Success(message);
        //}

        //protected void NotifySuccessLocalized(string key)
        //{
        //    NotyfService.Success(Localize(key));
        //}

        //protected void NotifyError(string message)
        //{
        //    NotyfService.Error(message);
        //}

        //protected void NotifyErrorLocalized(string key)
        //{
        //    NotyfService.Error(Localize(key));
        //}

        //protected void NotifyWarning(string message)
        //{
        //    NotyfService.Warning(message);
        //}

        //protected void NotifyWarningLocalized(string key)
        //{
        //    NotyfService.Warning(Localize(key));
        //}

        //protected string Localize(string key)
        //{
        //    return Localizer.GetString(key);
        //}
    }
}
