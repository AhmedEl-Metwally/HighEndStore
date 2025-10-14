using AutoMapper;
using Domain.Entities.ProductModule;
using Shared.Dtos.ProductsDto;

namespace Services.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductType,TypeResultDto>();
            CreateMap<ProductBrand,BrandResultDto>();
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl,option => option.MapFrom<PictureUrlResolver>());    
        }
    }
}
