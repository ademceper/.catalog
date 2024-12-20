public class Product : BaseEntity
{
    public string Name { get; set; }  // Ürün adı
    public string Slug { get; set; }  // SEO dostu URL (slug)
    public string Description { get; set; }  // Ürün açıklaması
    public string Info { get; set; }
    public decimal Price { get; set; }  // Ürün fiyatı
    public decimal? DiscountedPrice { get; set; }  // İndirimli fiyat (varsa)
    public decimal OriginalPrice { get; set; }  // Ürünün orijinal fiyatı
    public int StockQuantity { get; set; }  // Mevcut stok miktarı
    public bool IsFeatured { get; set; }  // Öne çıkan ürün mü?
    public List<ProductVariant> Variants { get; set; }  // Ürün varyantlar
    public Guid CategoryId { get; set; }  // Kategori ID'si
    public Category Category { get; set; }  // Ürünün kategorisi (Navigation property)
    public List<ProductImage> Images { get; set; }  // Ürün fotoğrafları
    public List<ProductAttribute> Attributes { get; set; }  // Ürün özellikleri (örneğin: renk, beden vb.)
    public string SearchIndex { get; set; }  // Elasticsearch arama endeksi için alan
    public bool IsSearchable { get; set; }  // Ürünün Elasticsearch'ün arama sistemine dahil olup olmadığı
}
