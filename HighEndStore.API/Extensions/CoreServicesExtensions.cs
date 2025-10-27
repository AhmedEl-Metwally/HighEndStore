using Services.Abstraction.Interface;
using Services.Implementation;
using Services.Mapping;
using Shared.Common;

namespace HighEndStore.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(Mapping =>
            {
                Mapping.AddProfile(new ProductProfile());
                Mapping.AddProfile(new BasketMapping());
            });

            services.AddTransient<PictureUrlResolver>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtOption>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
