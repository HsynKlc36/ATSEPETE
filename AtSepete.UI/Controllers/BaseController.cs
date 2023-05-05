using AspNetCoreHero.ToastNotification.Abstractions;
using AtSepete.Business.JWT;
using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.ApiResponses;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class BaseController:Controller
    {

        protected string? UserId => HttpContext.User.FindFirstValue("ID");
        protected string? UserRole => HttpContext.User.FindFirstValue(ClaimTypes.Role);
        protected string? UserName => HttpContext.User.FindFirstValue(ClaimTypes.Name);
        protected string? UserEmail => HttpContext.User.FindFirstValue(ClaimTypes.Email);
        protected string? UserToken => HttpContext.User.FindFirstValue("Token");

        
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
