public class CategoryCreateDto
{
    public string Name { get; set; }  // Kategori adı
    public string Description { get; set; }  // Kategori açıklaması
    public Guid? ParentCategoryId { get; set; }  // Üst kategori ID'si (nullable)
}
