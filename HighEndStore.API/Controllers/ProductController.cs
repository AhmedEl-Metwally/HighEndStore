using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interface;
using Shared.Dtos.ProductsDto;

namespace HighEndStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProductsAsync(int? typeId, int? brandId)
               => Ok(await _serviceManager.ProductService.GetAllProductsAsync(typeId,brandId));

        [HttpGet("Brand")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandAsync()
               => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        [HttpGet("Type")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypeAsync()
               => Ok(await _serviceManager.ProductService.GetAllTypesAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
    }
}
