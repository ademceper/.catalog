public class ProductSearchIndex : BaseEntity
{
    public string Name { get; set; }  // Ürün adı
    public string Slug { get; set; }  // SEO dostu URL
    public string Description { get; set; }  // Ürün açıklaması
    public decimal Price { get; set; }  // Ürün fiyatı
    public Guid CategoryId { get; set; }  // Kategori ID'si
    public string CategoryName { get; set; }  // Kategori adı
    public bool IsActive { get; set; }  // Ürün durumu
    public string Tags { get; set; }  // Etiketler (Elasticsearch için optimize edilmiş format)
}
