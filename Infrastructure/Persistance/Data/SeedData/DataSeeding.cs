using Domain.Contracts.SeedData;
using Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Context;
using System.Text.Json;

namespace Persistance.Data.SeedData
{
    public class DataSeeding(HighEndStoreDbContext _context) : IDataSeeding
    {
        public void SeedData()
        {
			try
			{
				if(_context.Database.GetPendingMigrations().Any())
					_context.Database.Migrate();
				if (!_context.ProductBrands.Any())
				{
					var ProductBrandsData = File.ReadAllText("..\\Infrastructure\\Persistance\\Data\\SeedData\\DataSeed\\brands.json");
					var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
					if (ProductBrands is not null && ProductBrands.Any())
						_context.ProductBrands.AddRange(ProductBrands);
                }
                if (!_context.ProductTypes.Any())
                {
                    var ProductTypesData = File.ReadAllText("..\\Infrastructure\\Persistance\\Data\\SeedData\\DataSeed\\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        _context.ProductTypes.AddRange(ProductTypes);
                }
                if (!_context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("..\\Infrastructure\\Persistance\\Data\\SeedData\\DataSeed\\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                        _context.Products.AddRange(Products);
                }
                _context.SaveChanges();
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
