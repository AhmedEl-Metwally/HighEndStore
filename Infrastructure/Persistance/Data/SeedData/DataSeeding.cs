using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Persistance.Data.SeedData
{
    public class DataSeeding(
                                HighEndStoreDbContext _context,
                                RoleManager<IdentityRole> _roleManager,
                                UserManager<User> _userManager
                            ) : IDataSeeding
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

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var adminUser = new User()
                    {
                        DisplayName = "Admin User",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01091399362"
                    };

                    var superAdmin = new User()
                    {
                        DisplayName = "Super Admin",
                        UserName = "Super",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01001399362"
                    };

                    adminUser = await _userManager.FindByEmailAsync("Admin@gmail.com");
                    superAdmin = await _userManager.FindByEmailAsync("SuperAdmin@gmail.com");

                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                }

            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
