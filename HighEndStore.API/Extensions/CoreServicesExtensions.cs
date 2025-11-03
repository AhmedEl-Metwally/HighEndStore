using Microsoft.Extensions.DependencyInjection;
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
                Mapping.AddProfile(new OrderMapping());
            });

            services.AddTransient<PictureUrlResolver>();
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<Func<IProductService>>(provider => ()=> provider.GetRequiredService<IProductService>());

            services.AddScoped<IBasketService,BasketService>();
            services.AddScoped<Func<IBasketService>>(provider => ()=> provider.GetRequiredService<IBasketService>());

            services.AddScoped<IAuthenticationService,AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider => ()=> provider.GetRequiredService<IAuthenticationService>());

            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<Func<IOrderService>>(provider => ()=> provider.GetRequiredService<IOrderService>());

            services.AddScoped<IPaymentService,PaymentService>();
            services.AddScoped<Func<IPaymentService>>(provider => ()=> provider.GetRequiredService<IPaymentService>());

            services.Configure<JwtOption>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
