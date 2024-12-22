using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()  
            .HasMaxLength(255); 

        builder.Property(c => c.Description)
            .HasMaxLength(1000); 

        builder.HasOne(c => c.ParentCategory) 
            .WithMany(c => c.SubCategories) 
            .HasForeignKey(c => c.ParentCategoryId) 
            .OnDelete(DeleteBehavior.SetNull);  

        builder.HasMany(c => c.Products)  
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId); 

        builder.Navigation(c => c.SubCategories) 
            .UsePropertyAccessMode(PropertyAccessMode.Property); 
    }
}
