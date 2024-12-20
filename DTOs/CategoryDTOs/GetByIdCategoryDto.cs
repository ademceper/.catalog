public class GetByIdCategoryDto
{
    public Guid Id { get; set; }  // Kategori ID'si
    public string Name { get; set; }  // Kategori adı
    public string Description { get; set; }  // Kategori açıklaması
    public Guid? ParentCategoryId { get; set; }  // Üst kategori ID'si (nullable)
    public string ParentCategoryName { get; set; }  // Üst kategori adı (isteğe bağlı)
    public List<GetByIdCategoryDto> SubCategories { get; set; } = new List<GetByIdCategoryDto>();  // Alt kategoriler
    public List<Product> Products { get; set; } = new List<Product>();  // Kategoriye ait ürünler
    public DateTime CreatedAt { get; set; }  // Oluşturulma tarihi
    public DateTime UpdatedAt { get; set; }  // Güncellenme tarihi
    public DateTime? DeletedAt { get; set; }  // Silinme tarihi (Soft delete)
    public bool IsActive { get; set; }  // Entity aktif mi?
    public string CreatedBy { get; set; }  // Kaydı oluşturan kullanıcı
    public string UpdatedBy { get; set; }  // Kaydı güncelleyen kullanıcı
    public string DeletedBy { get; set; }  // Kaydı silen kullanıcı
}
