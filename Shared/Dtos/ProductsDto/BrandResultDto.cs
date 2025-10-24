namespace Shared.DTOS.ProductsDto
{
    public record BrandResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
