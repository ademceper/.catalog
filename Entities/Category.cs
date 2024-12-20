public class Category : BaseEntity
{
    public string Name { get; set; }  // Kategori adı
    public string Description { get; set; }  // Kategori açıklaması
    public Guid? ParentCategoryId { get; set; }  // Üst kategori ID'si (nullable)
    public Category ParentCategory { get; set; }  // Üst kategori (Navigation property)
    public List<Category> SubCategories { get; set; } = new List<Category>();  // Alt kategoriler, varsayılan olarak boş liste
    public List<Product> Products { get; set; } = new List<Product>();
}
