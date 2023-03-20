using AtSepete.Business.Abstract;
using AtSepete.Business.Concrete;

namespace AtSepete.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services) 
        {
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
