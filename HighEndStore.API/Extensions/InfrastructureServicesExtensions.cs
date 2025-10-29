using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.SeedData;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.BasketRepositorys;
using Persistance.Data.Context;
using Persistance.Data.SeedData;
using Persistance.Identity;
using Persistance.Repositories.UnitOfWorks;
using Services.Abstraction.Interface;
using Services.Implementation;
using Shared.Common;
using StackExchange.Redis;
using System.Text;

namespace HighEndStore.API.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<HighEndStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });

            services.AddDbContext<IdentityHighEndStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
            });

            services.AddSingleton<IConnectionMultiplexer>((_) => 
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });

            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDataProtection();
            services.ValidateJwtToken(configuration);
            services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityHighEndStoreDbContext>().AddDefaultTokenProviders();

            return services;
        }


        public static IServiceCollection ValidateJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtOptions").Get<JwtOption>();
            services.AddAuthentication(option => 
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                };
            });
            services.AddAuthentication();
            return services;
        } 

    }
}

