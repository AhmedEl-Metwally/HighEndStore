using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.SeedData;
using Domain.Contracts.UnitOfWorks;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.BasketRepositorys;
using Persistance.Data.Context;
using Persistance.Data.SeedData;
using Persistance.Identity;
using Persistance.Repositories.UnitOfWorks;
using Services.Abstraction.Interface;
using Services.Implementation;
using StackExchange.Redis;

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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDataProtection();
            services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityHighEndStoreDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}

