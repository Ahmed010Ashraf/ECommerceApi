using Domain.Contracts;
using Domain.Models.identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistance.Data;
using Presistance.Reposatories;
using Shared.DTOs.Identity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public static class InfrustructureServiceRegisteration
    {
        public static IServiceCollection AddInfrustructure (this IServiceCollection services , IConfiguration config)
        {
            //register dbcontext
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            

            //register identity dbcontext
              services.AddDbContext<StoreIdentityDbContext>(options =>
              {
                  options.UseSqlServer(config.GetConnectionString("IdentityConnection"));
              });

            //register dbinitializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            //register unit of work
            services.AddScoped<IUniteOfWork, UniteOfWork>();


            //register redis
            services.AddSingleton<IConnectionMultiplexer>((options) =>
            {
                var redisConnectionString = config.GetConnectionString("RedisConnections");
                return ConnectionMultiplexer.Connect(redisConnectionString!);
            });

            //register ibasket reposatory
            services.AddScoped<IBasketRepository , BasketRepository >();

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            //configure jwt authentication
            services.ConfigerJwt(config);

            return services;
        }

        public static IServiceCollection ConfigerJwt ( this IServiceCollection services, IConfiguration config)
        {
            //register jwt options
           var jwtoptions = config.GetSection("JWTOptions").Get<JWTOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtoptions.Issuer,
                    ValidAudience = jwtoptions.Audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.Key)),

               };
            });
            services.AddAuthorization();

            return services;
        }
    }
}
