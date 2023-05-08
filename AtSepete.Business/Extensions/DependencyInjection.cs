using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Business.Logger;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

namespace AtSepete.Business.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJWTBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();//servislerde httpContext'e ulaşabilmek için
            services.AddScoped<ITokenHandler, JWT.TokenHandler>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                
            })
            .AddJwtBearer("Admin", options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,// hangi sitelerin veya kimlerin kullancağını belirleriz
                    ValidateIssuer = true,//oluşturulacak token değerini kimin dağıttığının belirlendiği yerdir
                    ValidateLifetime = true,//oluşturulan token değerinin süresini kontrol edecek olan doğrulama
                    ValidateIssuerSigningKey = true,//üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key  verisinin doğrulanmasıdır
                    ValidAudience = configuration["Token:Audience"],
                    ValidIssuer = configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    //expires=> gelen jwt=token=accessToken nin ömrüne bakar.eğer ki süresini doldurmuşsa kullanılamaz.
                    NameClaimType = ClaimTypes.NameIdentifier, //=>jwt üzerinde Name claim'e karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.Yani hangi kullanıcının istek yaptığını bu property sayesinde user.Identity.Name ile cağırdığımız yerde yakalamamıza yardımcı olur
                  

                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
                        // Kontrol etmek istediğiniz roller veya izinler burada belirtilir.
                        if (!claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
            })
               .AddJwtBearer("Customer", options =>
               {
                   options.TokenValidationParameters = new()
                   {
                       ValidateAudience = true,// hangi sitelerin veya kimlerin kullancağını belirleriz
                       ValidateIssuer = true,//oluşturulacak token değerini kimin dağıttığının belirlendiği yerdir
                       ValidateLifetime = true,//oluşturulan token değerinin süresini kontrol edecek olan doğrulama
                       ValidateIssuerSigningKey = true,//üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key  verisinin doğrulanmasıdır
                       ValidAudience = configuration["Token:Audience"],
                       ValidIssuer = configuration["Token:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                       LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                       //expires=> gelen jwt=token=accessToken nin ömrüne bakar.eğer ki süresini doldurmuşsa kullanılamaz.
                      
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnTokenValidated = context =>
                       {
                           var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
                           //süresi dolan tokeni yönlendirir.
                           var expirationClaim = claimsIdentity.FindFirst("exp");

                           if (expirationClaim != null && DateTime.TryParse(expirationClaim.Value, out var expirationDate))
                           {
                               if (expirationDate < DateTime.UtcNow)
                               {
                                   context.Response.Redirect("https://localhost:7286/AtSepeteApi/Login/RefreshTokenLogin");
                               }
                           }

                           // Kontrol etmek istediğiniz roller veya izinler burada belirtilir.
                           if (!claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Customer"))
                           {
                               context.Fail("Unauthorized");
                           }


                           return Task.CompletedTask;
                       }
                   };
               })
               .AddJwtBearer("ForgetPassword", options =>
               {
                   options.TokenValidationParameters = new()
                   {
                       ValidateAudience = true,// hangi sitelerin veya kimlerin kullancağını belirleriz
                       ValidateIssuer = true,//oluşturulacak token değerini kimin dağıttığının belirlendiği yerdir
                       ValidateLifetime = true,//oluşturulan token değerinin süresini kontrol edecek olan doğrulama
                       ValidateIssuerSigningKey = true,//üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key  verisinin doğrulanmasıdır
                       ValidAudience = configuration["Token:Audience"],
                       ValidIssuer = configuration["Token:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                       LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                       //expires=> gelen jwt=token=accessToken nin ömrüne bakar.eğer ki süresini doldurmuşsa kullanılamaz.
                      
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnTokenValidated = context =>
                       {
                           var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
                           // Kontrol etmek istediğiniz roller veya izinler burada belirtilir.
                           if (!claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "ForgetPassword"))
                           {
                               context.Fail("Unauthorized");
                              
                           }
                           var email = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                           if (!claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Email && c.Value == email))
                           {
                               context.Fail("Unauthorized");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });
            //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            //{

            //    app.Use(async (context, next) =>
            //    {
            //        if (!context.Request.Path.StartsWithSegments("/login") &&
            //        !context.Request.Path.StartsWithSegments("/logout"))
            //        {
            //            var cookieValue = context.Request.Cookies["AtSepeteCookie"]; // mycookie yerine kendi cookie adınızı kullanın

            //            if (string.IsNullOrEmpty(cookieValue))
            //            {
            //                context.Response.Redirect("/login"); // Cookie yoksa login sayfasına yönlendir
            //                return;
            //            }

            //            try
            //            {
            //                var token = JwtSecurityTokenHandler().ReadJwtToken(cookieValue);


            //                if (token.ValidTo <= DateTime.UtcNow)
            //                {
            //                    context.Response.Redirect("/login"); // Token süresi dolmuşsa login sayfasına yönlendir
            //                    return;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                // Token doğrulama hatası, kullanıcıyı login sayfasına yönlendir
            //                context.Response.Redirect("/login");
            //                return;
            //            }
            //        }



            //        await next();
            //    });



            //    ...
            //}


            services.AddAutoMapper(
            Assembly.GetExecutingAssembly(),
            typeof(CategoryProfile).Assembly,
            typeof(MarketProfile).Assembly,
            typeof(OrderProfile).Assembly,
            typeof(OrderDetailProfile).Assembly,
            typeof(ProductMarketProfile).Assembly,
            typeof(ProductProfile).Assembly,
            typeof(UserProfile).Assembly
            );
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMarketService, MarketService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductMarketService, ProductMarketService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IEmailSender, EmailSenderService>();

            return services;
        }
        #region CookieServices
//public static IServiceCollection AddCookieMVCServices(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddHttpContextAccessor();//servislerde httpContext'e ulaşabilmek için
        //    services.AddScoped<ITokenHandler, JWT.TokenHandler>();

        //    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie(options =>
        //    {
        //        options.SlidingExpiration = false;
        //        options.Cookie.Name = "AtSepeteCookie";
        //        options.ExpireTimeSpan = TimeSpan.FromMinutes(135);
        //        options.AccessDeniedPath = "/Home/ErişimEngellendi";
        //        options.LoginPath = "/Login/Login"; // Kimlik doğrulama başarısız olduğunda yönlendirme yapılacak sayfa
        //        options.Cookie.HttpOnly = true;
        //    });

        //    services.AddAutoMapper(
        //    Assembly.GetExecutingAssembly(),
        //    typeof(CategoryProfile).Assembly,
        //    typeof(MarketProfile).Assembly,
        //    typeof(OrderProfile).Assembly,
        //    typeof(OrderDetailProfile).Assembly,
        //    typeof(ProductMarketProfile).Assembly,
        //    typeof(ProductProfile).Assembly,
        //    typeof(UserProfile).Assembly
        //    );
        //    services.AddScoped<ICategoryService, CategoryService>();
        //    services.AddScoped<IMarketService, MarketService>();
        //    services.AddScoped<IProductService, ProductService>();
        //    services.AddScoped<IOrderService, OrderService>();
        //    services.AddScoped<IProductMarketService, ProductMarketService>();
        //    services.AddScoped<IOrderDetailService, OrderDetailService>();
        //    services.AddScoped<IUserService, UserService>();
        //    services.AddSingleton<ILoggerService, LoggerService>();
        //    services.AddSingleton<IEmailSender, EmailSenderService>();

        //    return services;
        //}
        #endregion
        
    }
}
