public class GetAllProductDto
{
    public Guid Id { get; set; }  // Ürün ID'si
    public string Name { get; set; }  // Ürün adı
    public string Slug { get; set; }  // SEO dostu URL (slug)
    public decimal Price { get; set; }  // Ürün fiyatı
    public bool IsFeatured { get; set; }  // Öne çıkan ürün mü?
}
