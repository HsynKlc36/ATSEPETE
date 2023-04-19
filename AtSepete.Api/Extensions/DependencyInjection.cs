using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.JWT;
using AtSepete.Business.Logger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using System.Text;

namespace AtSepete.Api.Extensions
{
    public static class DependencyInjection
    {
      
        public static IServiceCollection AddApiServices(this IServiceCollection services,IConfiguration configuration)
        {
            
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));//loglama
         
           
            return services;
        }
    }
}
