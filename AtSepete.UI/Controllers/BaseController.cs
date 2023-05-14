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
    public class BaseController : Controller
    {

        #region Protected Kullanımı!
        //Öncelikle protected erişim belirleyicisini hatırlama mahiyetinde ele alalım.
        //Bir sınıf içerisinde protected olarak işaretlenen bir eleman sadece o sınıf içinden yahut o sınıftan kalıtım alan sınıflar içerisinden erişilebilir olmaktadır.Yani o classın instanceı için private, o sınıftan türeyen sınıfların ve o sınıf içinde ise public özellik göstermektedir.

        //Not:Protected metotlar aynı namespace içerisinde kendisinden kalıtım alan sınıflarda public özelliktedirler, kendi içlerinde ve  kalıtım alınan yerlerde kullanılabilirler.Fakat kendi instance larında ve kalıtım verdikleri sınıfların instance larında kullanılamazlar.Ayrıca Private Protected olarak işaretlenen metot ya da property'ler sadece kendi namespace'i içerisinde kalıtım verdiği yerde yine public özellik gösterecektir farklı dll yani namespace'lerde kalıtım verilse dahi oralarda, atadan gelen protected metotlara ya da property'lere, private protected olarak işaretlendiğinde sadece farklı dll'lerden kalıtım yoluyla erişilmesi engellenir ve ulaşılamayacaktır!Burada ayrıca protected üye içeren class'ların kendilerinden ya da kalıtım verdikleri class'lardan doğrudan alınan instace' larda bu protected metot ve prop'lara erişim sağlanamaz(private)!Yani kalıtım dahi alsan doğrudan kalıtım aldığın nesneyi yarattığın(instance) zaman artık burada ulaşılamayacaktır.
        #endregion
        protected string? UserId => HttpContext.User.FindFirstValue("UserId");
        protected string? UserRole => HttpContext.User.FindFirstValue(ClaimTypes.Role);
        protected string? UserName => HttpContext.User.FindFirstValue(ClaimTypes.Name);
        protected string? UserEmail => HttpContext.User.FindFirstValue(ClaimTypes.Email);
        protected string? UserToken => HttpContext.User.FindFirstValue("Token");
        protected string? UserRefreshToken => HttpContext.User.FindFirstValue("RefreshToken");


        protected INotyfService NotyfService => HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;
        /*protected IStringLocalizer<SharedModelResource> Localizer => HttpContext.RequestServices.GetService(typeof(IStringLocalizer<SharedModelResource>)) as IStringLocalizer<SharedModelResource>;*/

        protected void NotifySuccess(string message)
        {

            NotyfService.Success(message);
        }

        //protected void NotifySuccessLocalized(string key)
        //{
        //    NotyfService.Success(Localize(key));
        //}

        protected void NotifyError(string message)
        {
            NotyfService.Error(message);
        }

        //protected void NotifyErrorLocalized(string key)
        //{
        //    NotyfService.Error(Localize(key));
        //}

        protected void NotifyWarning(string message)
        {
            NotyfService.Warning(message);
        }

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
