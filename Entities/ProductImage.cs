public class ProductImage : BaseEntity
{
    public string ImageUrl { get; set; }  // Fotoğraf URL'si
    public bool IsPrimary { get; set; }  // Fotoğrafın ana fotoğraf olup olmadığı
}
