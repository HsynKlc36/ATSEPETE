using AspNetCoreHero.ToastNotification.Abstractions;
using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class BaseController:Controller
    {
        protected string? UserIdentityId => User.FindFirstValue(ClaimTypes.NameIdentifier);
      
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
