public class ProductAttribute : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public string AttributeName { get; set; }  // Özellik adı (örneğin: renk, beden)
    public string AttributeValue { get; set; }  // Özellik değeri (örneğin: kırmızı, M)
}
