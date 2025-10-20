using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interface;
using Shared;
using Shared.Dtos.ProductsDto;
using Shared.ErrorModels;
using Shared.Specifications;

namespace HighEndStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationParameters parameters)
               => Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));

        [HttpGet("Brand")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandAsync()
               => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        [HttpGet("Type")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypeAsync()
               => Ok(await _serviceManager.ProductService.GetAllTypesAsync());

        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
    }
}
