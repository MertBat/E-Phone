using E_Phone.BLL.Services.Abstract;
using E_Phone.BLL.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL
{
    public static class ServiceRegistrations
    {
        public static void ServiceInjections(this IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandService>();
        }
    }
}
