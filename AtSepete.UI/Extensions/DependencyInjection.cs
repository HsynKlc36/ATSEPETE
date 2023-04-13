
using AspNetCoreHero.ToastNotification;
using AtSepete.UI.MapperUI.Profiles;
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
    }
}
