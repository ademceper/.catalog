public class ProductVariantSearchIndex : BaseEntity
{
    public string Name { get; set; }  // Varyant adı
    public string Slug { get; set; }  // SEO dostu URL
    public string Description { get; set; }  // Varyant açıklaması
    public decimal Price { get; set; }  // Fiyat
    public Guid ProductId { get; set; }  // Ürün ID'si
    public string CategoryName { get; set; }  // Kategori adı
    public bool IsActive { get; set; }  // Varyant durumu
}
