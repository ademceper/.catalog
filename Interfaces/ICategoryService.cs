public interface ICategoryService
{
    Task<ResultDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto, string createdBy);  // Kategori oluşturma
    Task<ResultDto> GetCategoryByIdAsync(Guid id);  // ID'ye göre kategori getirme
    Task<ResultDto> GetAllCategoriesAsync();  // Tüm kategorileri getirme
    Task<ResultDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryUpdateDto, string updatedBy);  // Kategori güncelleme
    Task<ResultDto> DeleteCategoryAsync(Guid id, string deletedBy);  // Kategori silme
}
