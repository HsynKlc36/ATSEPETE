using AtSepete.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.DataAccess.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AtSepeteDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(AtSepeteDbContext.ConnectionName),
                    options => options.EnableRetryOnFailure(
                        10,
                        TimeSpan.FromSeconds(10),
                        null));
                options.UseLazyLoadingProxies();
            });

            return services;
        }
    }
}
