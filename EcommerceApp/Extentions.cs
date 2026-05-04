using Domain.Contracts;
using EcommerceApp.factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace EcommerceApp
{
    public static class Extentions
    {
        public static IServiceCollection AddWepAppServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //handel the validation error of the modelstate 

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    return ApiResponseFactories.GenerateErrorValidationResponse(context);
                };
            });



            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                // 🔐 Add JWT Bearer definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid JWT token.\n\nExample: **Bearer eyJhbGciOiJIUzI1NiIsInR5cCI...**"
                });

                // 🔐 Require JWT authentication in Swagger
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
});
            });

            return services;

        }

        public static async Task DataBaseInitializer(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentity();
        }
    }
}
