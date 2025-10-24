using AutoMapper;
using Domain.Entities.BasketModule;
using Shared.DTOS.BasketsDto;

namespace Services.Mapping
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
        }
    }
}
