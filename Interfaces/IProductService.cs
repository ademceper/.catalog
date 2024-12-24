public interface IProductService
{
    Task<ResultDto> CreateProductAsync(ProductCreateDto productCreateDto, string createdBy, CancellationToken cancellationToken = default);
    Task<ResultDto> UpdateProductAsync(ProductUpdateDto productUpdateDto, string updatedBy, CancellationToken cancellationToken = default);
    Task<ResultDto> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ResultDto> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<ResultDto> DeleteProductAsync(Guid id, string deletedBy, CancellationToken cancellationToken = default);
    Task<ResultDto> SearchProductsAsync(string query, CancellationToken cancellationToken = default);
}