using AutoMapper;
using Domain.Entities.OrderModule;
using Shared.DTOS.OrderDto;
using Address = Domain.Entities.IdentityModule.Address;
using ShippingAddress = Domain.Entities.OrderModule.Address;

namespace Services.Mapping
{
    public class OrderMapping :Profile
    {
        public OrderMapping()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<DeliveryMethod,DeliveryMethodResultDto>();

            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.ProductInOrderItem.ProductId))
               .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ProductInOrderItem.ProductName))
               .ForMember(dest => dest.ProductUrl, option => option.MapFrom(src => src.ProductInOrderItem.ProductUrl));

            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.PaymentStatus, option => option.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, option => option.MapFrom(src => src.SubTotal + src.DeliveryMethod.Price));


        }
    }
}
