public class ProductVariantCreateDto
{
    public string VariantName { get; set; }  // Varyant adı (örneğin, renk, beden)
    public string VariantValue { get; set; }  // Varyant değeri (örneğin, kırmızı, M)
    public decimal Price { get; set; }  // Varyantın fiyatı
    public decimal? DiscountedPrice { get; set; }  // Varyantın indirimli fiyatı
    public int StockQuantity { get; set; }  // Varyant için mevcut stok miktarı
    public string SKU { get; set; }  // Ürün varyantı için SKU (Stok Kod Numarası)
    public string ImageUrl { get; set; }  // Varyantın resim URL'si (isteğe bağlı)
    public List<ProductVariantAttributeCreateDto> Attributes { get; set; }  // Varyantın özellikleri
}
