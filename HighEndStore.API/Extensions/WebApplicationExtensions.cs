using Domain.Contracts.SeedData;

namespace HighEndStore.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> AddSeedDataAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var objOfSeedData = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objOfSeedData.SeedDataAsync();
            await objOfSeedData.SeedDataAsync();
            return webApplication;
        }
    }
}
