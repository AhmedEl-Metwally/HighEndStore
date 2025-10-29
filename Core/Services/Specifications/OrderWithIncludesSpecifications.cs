using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    public class OrderWithIncludesSpecifications : BaseSpecifications<Order,Guid>
    {
        public OrderWithIncludesSpecifications(Guid id) : base(O => O.Id == id )
        {
            AddIncludes(O => O.DeliveryMethod);
            AddIncludes(O => O.OrderItems);
        }

        public OrderWithIncludesSpecifications(string userEmail) : base(O => O.UserEmail == userEmail)
        {
            AddIncludes(O => O.DeliveryMethod);
            AddIncludes(O => O.OrderItems);
            AddOrderBy(O => O.OrderDate);
        }
    }
}
