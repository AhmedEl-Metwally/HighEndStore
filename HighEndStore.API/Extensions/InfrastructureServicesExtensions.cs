using Domain.Contracts.BasketRepositorys;
using Domain.Contracts.SeedData;
using Domain.Contracts.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Persistance.BasketRepositorys;
using Persistance.Data.Context;
using Persistance.Data.SeedData;
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

            services.AddSingleton<IConnectionMultiplexer>((_) => 
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });

            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}

