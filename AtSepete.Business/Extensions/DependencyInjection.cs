using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;
using AtSepete.Business.Logger;
using AtSepete.Business.Mapper.Profiles;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
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
            return services;
        }
    }
}
