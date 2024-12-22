using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Ürün adı zorunlu ve 255 karakterle sınırlandırılmış
        builder.Property(p => p.Name)
            .IsRequired()  // Zorunlu alan
            .HasMaxLength(255);  // Maksimum uzunluk

        // SEO dostu URL (Slug) alanı zorunlu ve 255 karakterle sınırlandırılmış
        builder.Property(p => p.Slug)
            .IsRequired()  // Zorunlu alan
            .HasMaxLength(255);  // Maksimum uzunluk

        // Ürün açıklaması opsiyonel ve 1000 karakterle sınırlandırılmış
        builder.Property(p => p.Description)
            .HasMaxLength(1000);  // Maksimum uzunluk

        // Ürün bilgisi (Info) opsiyonel, ancak uzunluk sınırı koyulabilir
        builder.Property(p => p.Info)
            .HasMaxLength(1000);

        // Fiyatlar zorunlu
        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.OriginalPrice)
            .IsRequired();

        // İndirimli fiyat var ise, nullable olarak bırakılmış
        builder.Property(p => p.DiscountedPrice)
            .IsRequired(false);  // Nullable

        // Stok miktarı zorunlu
        builder.Property(p => p.StockQuantity)
            .IsRequired();

        // Öne çıkan ürün (IsFeatured) alanı zorunlu
        builder.Property(p => p.IsFeatured)
            .IsRequired();

        // Elasticsearch ile ilgili arama endeksi
        builder.Property(p => p.SearchIndex)
            .HasMaxLength(500);  // Maksimum uzunluk

        // IsSearchable alanı zorunlu
        builder.Property(p => p.IsSearchable) 
            .IsRequired();

        // Kategoriyi ilişkilendir
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);  // Kategori silindiğinde ürünler de silinsin

        // Varyantlar ile ilişkiyi belirt 
        builder.HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // Varyantlar silindiğinde ürün varyantları da silinsin

        // Ürün fotoğrafları ile ilişkiyi belirt
        builder.HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // Fotoğraflar silindiğinde ürün fotoğrafları da silinsin

        // Ürün özellikleri ile ilişkiyi belirt
        builder.HasMany(p => p.Attributes)
            .WithOne(a => a.Product)
            .HasForeignKey(a => a.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // Ürün özellikleri silindiğinde, özellikler de silinsin
    }
}
