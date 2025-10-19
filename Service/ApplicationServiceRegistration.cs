using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Service_Abstraction;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManger, ServiceManger>();
            Services.AddAutoMapper(cfg => { }, typeof(AssemplyRefefanceMapping).Assembly);
            return Services;

        }

    }
}
