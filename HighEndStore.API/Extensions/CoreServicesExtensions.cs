using Services.Abstraction.Interface;
using Services.Implementation;
using Services.Mapping;

namespace HighEndStore.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Mapping => Mapping.AddProfile(new ProductProfile()));
            services.AddTransient<PictureUrlResolver>();
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
