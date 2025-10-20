using HighEndStore.API.Factorys;
using Microsoft.AspNetCore.Mvc;

namespace HighEndStore.API.Extensions
{
    public static class WebApiServicesExtensions
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services) 
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponceFactory.CustomValidationErrorResponse;
            });

            return services;
        }
    }
}
