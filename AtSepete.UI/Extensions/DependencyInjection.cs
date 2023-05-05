
using AspNetCoreHero.ToastNotification;
using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Business.Logger;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.UI.MapperUI.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
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

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Login/Index");
                options.LogoutPath = new PathString("/Login/SignOut");
                options.Cookie = new CookieBuilder
                {
                    Name = "AtSepeteCookie",
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.AccessDeniedPath = new PathString("/AccessDenied");
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
                options.ExpireTimeSpan = TimeSpan.FromMinutes(135);
                options.AccessDeniedPath = "/Home/ErişimEngellendi";
                options.LoginPath = "/Login/Login"; // Kimlik doğrulama başarısız olduğunda yönlendirme yapılacak sayfa
                options.Cookie.HttpOnly = true;
            });

            return services;
        }
        public static IServiceCollection AddGoogleMVCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = configuration["AppGoogle:GoogleClientId"];
                    options.ClientSecret = configuration["AppGoogle:GoogleClientSecret"];
                });
            

            return services;
        }
    }
}
