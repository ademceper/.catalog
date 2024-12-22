public interface ICategoryService
{
    Task<ResultDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto, string createdBy, CancellationToken cancellationToken);  // Kategori oluşturma
    Task<ResultDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);  // ID'ye göre kategori getirme
    Task<ResultDto> GetAllCategoriesAsync( CancellationToken cancellationToken);  // Tüm kategorileri getirme
    Task<ResultDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryUpdateDto, string updatedBy, CancellationToken cancellationToken);  // Kategori güncelleme
    Task<ResultDto> DeleteCategoryAsync(Guid id, string deletedBy, CancellationToken cancellationToken);  // Kategori silme
}
