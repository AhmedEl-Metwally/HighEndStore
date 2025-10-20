using Domain.Contracts.SeedData;
using Domain.Contracts.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Context;
using Persistance.Data.SeedData;
using Persistance.Repositories.UnitOfWorks;

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

            services.AddScoped<IDataSeeding, DataSeeding>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
