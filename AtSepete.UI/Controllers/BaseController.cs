
using AtSepete.Business.JWT;
using AtSepete.Dtos.Dto.Users;
using AtSepete.UI.ApiResponses;
using AtSepete.UI.Models;
using AtSepete.UI.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using NToastNotify;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IToastNotification _toastNotification;
        private readonly IConfiguration _configuration;

        public BaseController(IToastNotification toastNotification,IConfiguration configuration)
        {
            _toastNotification = toastNotification;
            _configuration = configuration;
        }
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

        protected string? ApiBaseUrl => _configuration["Host:ApiBaseUrl"];

      
        protected IStringLocalizer<SharedModelResource> Localizer => HttpContext.RequestServices.GetService(typeof(IStringLocalizer<SharedModelResource>)) as IStringLocalizer<SharedModelResource>;

        //notifySuccess metotları Localized işlemi gerektirmeyenler yani dil çevirisine ihtiyac olmayanlar  controller'larda api'lardan gelen response içeriisindeki dönen mesajları doğrudan kullanacaktır fakat localized'li metotlar ise bu response'lardaki mesajları alıp resources içerisindeki resx lerde çeviri yaparak döndürecektir. 
        //IStringLocalizer servisi, çoklu dil desteği sağlamak ve yerelleştirme için kullanılan metinleri içeren kaynak dosyalarına erişim sağlar. Bu kaynak dosyaları, dil veya kültüre göre farklı metinleri depolar ve uygulamanın çalıştığı dil veya kültürüne bağlı olarak doğru metinleri döndürür.

        protected void NotifySuccess( string message, string title = "Başarılı")
        {
            _toastNotification.AddSuccessToastMessage(message, new ToastrOptions { Title=title});
        }


        protected void NotifySuccessLocalized(string message, string title = "Başarılı")
        {
            _toastNotification.AddSuccessToastMessage(Localize(message), new ToastrOptions { Title = title });
        }

        protected void NotifyError(string message,string title="Hata")
        {
            _toastNotification.AddErrorToastMessage(message, new ToastrOptions { Title = title });
        }

        protected void NotifyErrorLocalized(string message, string title = "Hata")
        {
            _toastNotification.AddErrorToastMessage(Localize(message), new ToastrOptions { Title = title });
        }

        protected void NotifyWarning(string message,string title="Uyarı")
        {
            _toastNotification.AddWarningToastMessage(message, new ToastrOptions { Title = title });
        }

        protected void NotifyWarningLocalized(string message, string title = "Uyarı")
        {
            _toastNotification.AddWarningToastMessage(Localize(message), new ToastrOptions { Title = title });
        }

        protected string Localize(string message)
        {
            return Localizer.GetString(message);//GetString metodu, verilen bir anahtar (key/message) değeriyle kaynak dosyasından ilgili metin öğesini alır. Bu metot, dil veya kültüre göre doğru metni döndürür. Eğer belirtilen anahtar, kaynak dosyasında bulunamazsa, varsayılan olarak anahtar değerini döndürür.
        }
    }
}
