using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Business.Logger;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.UI.MapperUI.Profiles;
using AtSepete.UI.Resources;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using NToastNotify;
using SendGrid;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace AtSepete.UI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions() {
                PositionClass = ToastPositions.BottomRight,
                TimeOut=5000,
                ShowDuration = 1000, // Bildirimin görüntülenme animasyon süresini milisaniye cinsinden belirler
                HideDuration = 1000, // Bildirimin kapanma animasyon süresini milisaniye cinsinden belirler
            })
            .AddRazorRuntimeCompilation().AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix,
            opt => opt.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                var assemblyName = new AssemblyName(typeof(SharedModelResource).GetTypeInfo().Assembly.FullName!);
                return factory.Create(nameof(SharedModelResource), assemblyName.Name!);
            });
            services.AddAutoMapper(
               Assembly.GetExecutingAssembly(),
               typeof(CategoryVMProfile).Assembly);
            //Bu mapper'lar incelenecek!!!!

            return services;
        }
        public static IServiceCollection AddCookieMVCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.SlidingExpiration = false;
                options.Cookie.Name = "AtSepeteCookie";
                options.ExpireTimeSpan = TimeSpan.FromHours(30);
                //options.AccessDeniedPath = "/Home/ErişimEngellendi";
                options.LoginPath = "/Login/Login/"; // Kimlik doğrulama başarısız olduğunda yönlendirme yapılacak sayfa
                options.Cookie.HttpOnly = true;
            });


            return services;
        }
       
    }
}
