using Domain.Entities.OrderModule;

namespace Persistance.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress , S =>S.WithOwner());
            builder.HasMany(O =>O.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(O => O.PaymentStatus).HasConversion(PS =>PS.ToString(),
                PS => Enum.Parse<OrderPaymentStatus>(PS));
            builder.HasOne(O =>O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(O =>O.SubTotal).HasColumnType("decimal(18,4)");
        }
    }
}
