using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> CreateProductAsync(ProductCreateDto productCreateDto, string createdBy)
    {
        if (productCreateDto == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Ürün verisi eksik.",
                ValidationErrors = new List<string> { "Ürün bilgileri eksik." },
                Code = "400"
            };
        }

        // Yeni ürün oluşturuluyor
        var product = new Product
        {
            Name = productCreateDto.Name,
            Slug = productCreateDto.Slug,
            Description = productCreateDto.Description,
            Info = productCreateDto.Info,
            Price = productCreateDto.Price,
            DiscountedPrice = productCreateDto.DiscountedPrice,
            OriginalPrice = productCreateDto.OriginalPrice,
            StockQuantity = productCreateDto.StockQuantity,
            IsFeatured = productCreateDto.IsFeatured,
            CategoryId = productCreateDto.CategoryId,
            CreatedBy = createdBy,
            UpdatedBy = createdBy,
            SearchIndex = productCreateDto.SearchIndex,
            IsSearchable = productCreateDto.IsSearchable
        };

        if (productCreateDto.Variants != null && productCreateDto.Variants.Any())
        {
            product.Variants = productCreateDto.Variants.Select(v => new ProductVariant
            {
                VariantName = v.VariantName,
                VariantValue = v.VariantValue,
                Price = v.Price,
                DiscountedPrice = v.DiscountedPrice,
                StockQuantity = v.StockQuantity,
                SKU = v.SKU,
                ImageUrl = v.ImageUrl,
                CreatedBy = createdBy,
                UpdatedBy = createdBy,
                Attributes = v.Attributes.Select(a => new ProductVariantAttribute
                {
                    AttributeName = a.AttributeName,
                    AttributeValue = a.AttributeValue,
                    CreatedBy = createdBy,
                    UpdatedBy = createdBy
                }).ToList()
            }).ToList();
        }

        if (productCreateDto.Images != null && productCreateDto.Images.Any())
        {
            product.Images = productCreateDto.Images.Select(i => new ProductImage
            {
                ImageUrl = i.ImageUrl,
                IsPrimary = i.IsPrimary,
                CreatedBy = createdBy,
                UpdatedBy = createdBy
            }).ToList();
        }

        if (productCreateDto.Attributes != null && productCreateDto.Attributes.Any())
        {
            product.Attributes = productCreateDto.Attributes.Select(a => new ProductAttribute
            {
                AttributeName = a.AttributeName,
                AttributeValue = a.AttributeValue,
                CreatedBy = createdBy,
                UpdatedBy = createdBy
            }).ToList();
        }

        try
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                Success = true,
                Message = "Ürün başarıyla oluşturuldu.",
                Data = product.Id,
                Code = "201"
            };
        }
        catch (Exception ex)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Bir hata oluştu.",
                ErrorDetails = ex.Message,
                Code = "500"
            };
        }
    }

    public async Task<ResultDto> UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto, string updatedBy)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Ürün bulunamadı.",
                ValidationErrors = new List<string> { "Geçerli ürün bulunamadı." },
                Code = "404"
            };
        }

        product.Name = productUpdateDto.Name;
        product.Slug = productUpdateDto.Slug;
        product.Description = productUpdateDto.Description;
        product.Info = productUpdateDto.Info;
        product.Price = productUpdateDto.Price;
        product.DiscountedPrice = productUpdateDto.DiscountedPrice;
        product.OriginalPrice = productUpdateDto.OriginalPrice;
        product.StockQuantity = productUpdateDto.StockQuantity;
        product.IsFeatured = productUpdateDto.IsFeatured;
        product.CategoryId = productUpdateDto.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;
        product.UpdatedBy = updatedBy;

        try
        {
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                Success = true,
                Message = "Ürün başarıyla güncellendi.",
                Data = product.Id,
                Code = "200"
            };
        }
        catch (Exception ex)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Ürün güncellenemedi.",
                ErrorDetails = ex.Message,
                Code = "500"
            };
        }
    }

    public async Task<ResultDto> GetProductByIdAsync(Guid id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new GetByIdProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                Description = p.Description,
                Info = p.Info,
                Price = p.Price,
                DiscountedPrice = p.DiscountedPrice,
                OriginalPrice = p.OriginalPrice,
                StockQuantity = p.StockQuantity,
                IsFeatured = p.IsFeatured,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Ürün bulunamadı.",
                ValidationErrors = new List<string> { "Geçerli ürün ID'si bulunamadı." },
                Code = "404"
            };
        }

        return new ResultDto
        {
            Success = true,
            Message = "Ürün başarıyla getirildi.",
            Data = product,
            Code = "200"
        };
    }

    public async Task<ResultDto> GetAllProductsAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => new GetAllProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                Price = p.Price,
                IsFeatured = p.IsFeatured
            })
            .ToListAsync();

        return new ResultDto
        {
            Success = true,
            Message = "Ürünler başarıyla getirildi.",
            Data = products,
            Code = "200"
        };
    }

    public async Task<ResultDto> DeleteProductAsync(Guid id, string deletedBy)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Ürün bulunamadı.",
                ValidationErrors = new List<string> { "Geçerli ürün bulunamadı." },
                Code = "404"
            };
        }

        product.IsActive = false;
        product.DeletedAt = DateTime.UtcNow;
        product.DeletedBy = deletedBy;

        try
        {
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                Success = true,
                Message = "Ürün başarıyla silindi.",
                Data = product.Id,
                Code = "200"
            };
        }
        catch (Exception ex)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Ürün silinemedi.",
                ErrorDetails = ex.Message,
                Code = "500"
            };
        }
    }
}
