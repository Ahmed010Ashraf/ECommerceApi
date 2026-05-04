using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceImplementation.profiles;
using ServicesAbstraction;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public static class ApplicationServiceRegisterations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration config)
        {
            //register service manager
            services.AddScoped<IServiceManager, ServiceManager>();
            //register auto mapper
            services.AddAutoMapper(cfg =>
            {
            }, typeof(ProductProfile).Assembly);

            services.Configure<JWTOptions>(config.GetSection("JWTOptions"));

            
           
           


            //builder.Services.AddAutoMapper(p => p.AddProfile(new ProductProfile()));

            return services;
        }
    }
}
