public class GetByIdProductDto
{
    public Guid Id { get; set; }  // Ürün ID'si
    public string Name { get; set; }  // Ürün adı
    public string Slug { get; set; }  // SEO dostu URL (slug)
    public string Description { get; set; }  // Ürün açıklaması
    public string Info { get; set; }  // Ürün bilgisi
    public decimal Price { get; set; }  // Ürün fiyatı
    public decimal? DiscountedPrice { get; set; }  // İndirimli fiyat (varsa)
    public decimal OriginalPrice { get; set; }  // Ürünün orijinal fiyatı
    public int StockQuantity { get; set; }  // Mevcut stok miktarı
    public bool IsFeatured { get; set; }  // Öne çıkan ürün mü?
    public CategoryDto Category { get; set; }  // Kategori bilgisi
    public List<ProductImageDto> Images { get; set; }  // Ürün fotoğrafları
    public List<ProductVariantDto> Variants { get; set; }  // Ürün varyantları
    public List<ProductAttributeDto> Attributes { get; set; }  // Ürün özellikleri
    public string SearchIndex { get; set; }  // Elasticsearch arama endeksi
    public bool IsSearchable { get; set; }  // Elasticsearch'e dahil edilip edilmeyeceği
}
