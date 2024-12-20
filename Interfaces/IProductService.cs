public interface IProductService
{
    Task<ResultDto> CreateProductAsync(ProductCreateDto productCreateDto, string createdBy);
    Task<ResultDto> UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto, string updatedBy);
    Task<ResultDto> GetProductByIdAsync(Guid id);
    Task<ResultDto> GetAllProductsAsync();
    Task<ResultDto> DeleteProductAsync(Guid id, string deletedBy);
}
