
namespace Persistance.Data.SeedData
{
    public class DataSeeding(HighEndStoreDbContext _context) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
			try
			{
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
					await _context.Database.MigrateAsync();
				if (!_context.ProductBrands.Any())
				{
					var ProductBrandsData = File.OpenRead("..\\Infrastructure\\Persistance\\Data\\SeedData\\DataSeed\\brands.json");
					var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);
					if (ProductBrands is not null && ProductBrands.Any())
						await _context.ProductBrands.AddRangeAsync(ProductBrands);
                }
                if (!_context.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead("..\\Infrastructure\\Persistance\\Data\\SeedData\\DataSeed\\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        await _context.ProductTypes.AddRangeAsync(ProductTypes);
                }
                if (!_context.Products.Any())
                {
                    var ProductsData = File.OpenRead("..\\Infrastructure\\Persistance\\Data\\SeedData\\DataSeed\\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                        await _context.Products.AddRangeAsync(Products);
                }
                await _context.SaveChangesAsync();
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
