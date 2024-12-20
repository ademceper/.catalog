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

    // İlişkiler
    public List<Guid> VariantIds { get; set; }  // Varyant ID'leri
    public List<Guid> ImageIds { get; set; }  // Fotoğraf ID'leri
    public List<Guid> TagIds { get; set; }  // Etiket ID'leri
    public List<Guid> AttributeIds { get; set; }  // Özellik ID'leri

    // Elasticsearch Entegrasyonu
    public string SearchIndex { get; set; }  // Elasticsearch arama endeksi
    public bool IsSearchable { get; set; }  // Elasticsearch'ün arama sistemine dahil olup olmadığı
}
