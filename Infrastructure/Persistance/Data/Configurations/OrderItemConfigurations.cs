using Domain.Entities.OrderModule;

namespace Persistance.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(O => O.Price).HasColumnType("decimal(18,4)");
            builder.OwnsOne(O => O.ProductInOrderItem, P => P.WithOwner());
        }
    }
}
