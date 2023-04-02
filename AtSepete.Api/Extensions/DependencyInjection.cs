using AtSepete.Api.Jwt;
using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AtSepete.Api.Extensions
{
    public static class DependencyInjection
    {
      
        public static IServiceCollection AddApiServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenHandler, AtSepete.Api.Jwt.TokenHandler>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
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
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires!=null? expires>DateTime.UtcNow:false
                        //expires=> gelen jwt=token=accessToken nin ömrüne bakar.eğer ki süresini doldurmuşsa kullanılamaz.
                    };
                    
                });
            return services;
        }
    }
}
