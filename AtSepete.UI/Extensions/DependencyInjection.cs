using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Business.Logger;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.DataAccess.Context;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using AtSepete.UI.AdminConsumers;
using AtSepete.UI.Controllers;
using AtSepete.UI.FluentFilter;
using AtSepete.UI.FluentValidatiors.LoginValidatiors;
using AtSepete.UI.MapperUI.Profiles;
using AtSepete.UI.Middleware;
using AtSepete.UI.Resources;
using FluentValidation;
using FluentValidation.AspNetCore;
using FormHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SendGrid;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace AtSepete.UI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews(options=>options.Filters.Add<ValidationFilter>()).AddNToastNotifyToastr(new ToastrOptions()
            {
                PositionClass = ToastPositions.BottomRight,
                TimeOut = 5000,
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

            services.AddScoped<AdminMessageConsumer>();//rabbitmq

            services.AddControllersWithViews().AddFluentValidation(opt => 
            {
                opt.RegisterValidatorsFromAssemblyContaining<RegisterVMValidator>();
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
             });

            services.AddHttpContextAccessor();
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
                options.AccessDeniedPath = "/Login/AccessDeniedPage";/* new PathString("/Login/AccessDeniedPage")*/
                options.LoginPath = "/Login/Login/"; // Kimlik doğrulama başarısız olduğunda yönlendirme yapılacak sayfa
                options.Cookie.HttpOnly = true;
            });

            return services;
        }
       

    }
}
