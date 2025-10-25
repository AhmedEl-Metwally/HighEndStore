using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistance.Identity
{
    public class IdentityHighEndStoreDbContext(DbContextOptions<IdentityHighEndStoreDbContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses"); 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
