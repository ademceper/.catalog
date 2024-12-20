public class ProductVariantAttribute : BaseEntity
{
    public Guid ProductVariantId { get; set; }  // Varyant ID'si
    public ProductVariant ProductVariant { get; set; }  // İlgili ürün varyantı

    public string AttributeName { get; set; }  // Özellik adı (örneğin, renk, beden)
    public string AttributeValue { get; set; }  // Özellik değeri (örneğin, kırmızı, M)
}
