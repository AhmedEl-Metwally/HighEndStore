using AutoMapper;
using Domain.Entities.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.ProductsDto;

namespace Services.Mapping
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product,ProductResultDto,string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            //var pictureUrl = source.PictureUrl;
            return $"{_configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
