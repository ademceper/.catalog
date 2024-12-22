using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> CreateProductAsync(ProductCreateDto productCreateDto, string createdBy, CancellationToken cancellationToken = default)
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

        try
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken); 

            var productId = product.Id;

            if (productCreateDto.Variants != null)
            {
                foreach (var v in productCreateDto.Variants)
                {
                    var productVariant = new ProductVariant
                    {
                        ProductId = productId,
                        VariantName = v.VariantName,
                        VariantValue = v.VariantValue,
                        Price = v.Price,
                        DiscountedPrice = v.DiscountedPrice,
                        StockQuantity = v.StockQuantity,
                        SKU = v.SKU,
                        ImageUrl = v.ImageUrl,
                        CreatedBy = createdBy,
                        UpdatedBy = createdBy
                    };

                    if (v.Attributes != null)
                    {
                        productVariant.Attributes = v.Attributes.Select(a => new ProductVariantAttribute
                        {
                            AttributeName = a.AttributeName,
                            AttributeValue = a.AttributeValue,
                            CreatedBy = createdBy,
                            UpdatedBy = createdBy
                        }).ToList();
                    }

                    _context.ProductVariants.Add(productVariant);
                }
            }

            if (productCreateDto.Images != null)
            {
                foreach (var i in productCreateDto.Images)
                {
                    var productImage = new ProductImage
                    {
                        ProductId = productId,
                        ImageUrl = i.ImageUrl,
                        IsPrimary = i.IsPrimary,
                        CreatedBy = createdBy,
                        UpdatedBy = createdBy
                    };

                    _context.ProductImages.Add(productImage);
                }
            }

            if (productCreateDto.Attributes != null)
            {
                foreach (var a in productCreateDto.Attributes)
                {
                    var productAttribute = new ProductAttribute
                    {
                        ProductId = productId, 
                        AttributeName = a.AttributeName,
                        AttributeValue = a.AttributeValue,
                        CreatedBy = createdBy,
                        UpdatedBy = createdBy
                    };

                    _context.ProductAttributes.Add(productAttribute);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto
            {
                Success = true,
                Message = "Ürün ve ilişkili öğeler başarıyla oluşturuldu.",
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


    public async Task<ResultDto> UpdateProductAsync(Guid id, ProductUpdateDto productUpdateDto, string updatedBy, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);

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
            await _context.SaveChangesAsync(cancellationToken);
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

    public async Task<ResultDto> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var productDto = await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id && p.IsActive)
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
            .FirstOrDefaultAsync(cancellationToken);

        if (productDto == null)
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
            Data = productDto,
            Code = "200"
        };
    }

    public async Task<ResultDto> GetAllProductsAsync(CancellationToken cancellationToken = default)
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
            .ToListAsync(cancellationToken);

        return new ResultDto
        {
            Success = true,
            Message = "Ürünler başarıyla getirildi.",
            Data = products,
            Code = "200"
        };
    }

    public async Task<ResultDto> SearchProductsAsync(string query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return new ResultDto
            {
                Success = false,
                Message = "Geçersiz arama terimi.",
                ValidationErrors = new List<string> { "Arama terimi boş olamaz." },
                Code = "400"
            };
        }

        var products = await _context.Products
            .Where(p => p.IsActive && 
                        (p.Name.Contains(query) || p.Description.Contains(query))) 
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (products == null || !products.Any())
        {
            return new ResultDto
            {
                Success = false,
                Message = "Arama sonuçları bulunamadı.",
                ValidationErrors = new List<string> { "Arama kriterine uyan ürün bulunamadı." },
                Code = "404"
            };
        }

        var productDtos = products.Select(p => new GetAllProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Slug = p.Slug,
            Price = p.Price,
            IsFeatured = p.IsFeatured
        }).ToList();

        return new ResultDto
        {
            Success = true,
            Message = "Arama sonuçları başarıyla getirildi.",
            Data = productDtos,
            Code = "200"
        };
    }

    public async Task<ResultDto> DeleteProductAsync(Guid id, string deletedBy, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);

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
            await _context.SaveChangesAsync(cancellationToken);
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
