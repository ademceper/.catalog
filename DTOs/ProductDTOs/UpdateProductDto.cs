public class ProductUpdateDto
{
    public string Name { get; set; }  // Ürün adı
    public string Slug { get; set; }  // SEO dostu URL (slug)
    public string Description { get; set; }  // Ürün açıklaması
    public string Info { get; set; }  // Ürün bilgisi
    public decimal Price { get; set; }  // Ürün fiyatı
    public decimal? DiscountedPrice { get; set; }  // İndirimli fiyat (varsa)
    public decimal OriginalPrice { get; set; }  // Ürünün orijinal fiyatı
    public int StockQuantity { get; set; }  // Mevcut stok miktarı
    public bool IsFeatured { get; set; }  // Öne çıkan ürün mü?
    public Guid CategoryId { get; set; }  // Kategori ID'si
    public List<ProductVariantUpdateDto> Variants { get; set; }  // Ürün varyantları
    public List<ProductImageUpdateDto> Images { get; set; }  // Ürün fotoğrafları
    public List<ProductAttributeUpdateDto> Attributes { get; set; }  // Ürün özellikleri (örneğin: renk, beden vb.)
    public string SearchIndex { get; set; }  // Elasticsearch arama endeksi
    public bool IsSearchable { get; set; }  // Elasticsearch'e dahil edilip edilmeyeceği
}
