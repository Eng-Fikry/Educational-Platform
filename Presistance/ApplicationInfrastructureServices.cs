using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistance.DataSeeding;
using Presistance.Identity;
using Presistance.Reposatories;
using StackExchange.Redis;

namespace Presistance
{
    public static class ApplicationInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services,IConfiguration _configuration) 
        {
            Services.AddDbContext<PlatformDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("PlatformConnection"));
            });
            Services.AddDbContext<PlatformIdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            });
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(_configuration["ConnectionStrings:RedisConnection"]!);
            });

            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IDataSeed, DataSeed>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            return Services;
        }
    }
}
