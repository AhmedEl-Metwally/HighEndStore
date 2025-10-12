namespace Persistance.Data.Context
{
    public class HighEndStoreDbContext(DbContextOptions<HighEndStoreDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductType> ProductTypes{ get; set; }
        public DbSet<ProductBrand> ProductBrands{ get; set; }
    }
}



