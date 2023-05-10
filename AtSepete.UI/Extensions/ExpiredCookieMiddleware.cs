//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Extensions.Options;
//using System.Net;
//using System.Text;
//using System.Text.Json;

//namespace AtSepete.UI.Extensions
//{
//    public class ExpiredCookieMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly HttpContextAccessor _httpContextAccessor;

//        public ExpiredCookieMiddleware(RequestDelegate next, HttpContextAccessor httpContextAccessor)
//        {
//            _next = next;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            if (context.Request.Cookies.ContainsKey("AtSepeteCookie"))
//            {
//                var cookieValue = context.Request.Cookies["AtSepeteCookie"];

//                // Cookie'nin süresi dolmuşsa, tarayıcıdan silinir
//                if (IsExpired(cookieValue))
//                {
//                    context.Response.Cookies.Delete("AtSepeteCookie");
//                }
//            }

//            await _next(context);
//        }

//        private bool IsExpired(string cookieValue)
//        {

//            // Oluşturduğunuz cookie'yi çekin
            
//            DateTime expirationDate;
//            // Cookie varsa ve değeri boş değilse işlemleri yapın
//            if (!string.IsNullOrEmpty(cookieValue))
//            {
//                // Cookie'yi çözümleyin veya kullanmanız gereken veriyi elde edin
//                // Örneğin, tarih değerini almak için DateTime.Parse gibi bir işlem yapabilirsiniz
//                var decodedCookie = WebEncoders.Base64UrlDecode(cookieValue);
//                var decodedString = Encoding.UTF8.GetString(decodedCookie);
//                var cookieData = JsonSerializer.Deserialize<Dictionary<string, string>>(decodedString, null);
//                var expirationDateStr = cookieData["Expiration"];
//                var expirationDate = DateTime.Parse(expirationDateStr);


//                if (expirationDate < DateTime.UtcNow)
//                {
//                    return true; // Cookie süresi dolmuş
//                }
//            }

//            return false; // Cookie süresi dolmamış

//        }
//    }

//}
