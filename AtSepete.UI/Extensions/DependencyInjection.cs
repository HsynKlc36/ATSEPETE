
using AspNetCoreHero.ToastNotification;
using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Business.Logger;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.UI.MapperUI.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace AtSepete.UI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAutoMapper(
               Assembly.GetExecutingAssembly(),
               typeof(CategoryVMProfile).Assembly);

            services.AddNotyf(options =>
            {
                options.IsDismissable = true;
                options.Position = NotyfPosition.BottomRight;
                options.HasRippleEffect = true;
            });

          

            return services;
        }
        public static IServiceCollection AddCookieMVCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.SlidingExpiration = false;
                options.Cookie.Name = "AtSepeteCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                //options.AccessDeniedPath = "/Home/ErişimEngellendi";
                options.LoginPath = "/Login/Login"; // Kimlik doğrulama başarısız olduğunda yönlendirme yapılacak sayfa
                options.Cookie.HttpOnly = true;
            });


            return services;
        }
       
    }
}
