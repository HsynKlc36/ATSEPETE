using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoriesServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMarketRepository, MarketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
