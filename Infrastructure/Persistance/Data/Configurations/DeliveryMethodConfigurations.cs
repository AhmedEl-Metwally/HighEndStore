using Domain.Entities.OrderModule;

namespace Persistance.Data.Configurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D =>D.Price).HasColumnType("decimal(18,4)");
        }
    }
}
