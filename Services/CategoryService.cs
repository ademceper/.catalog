using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto, string createdBy, CancellationToken cancellationToken)
    {
        if (categoryCreateDto == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Kategori verisi eksik.",
                ValidationErrors = new List<string> { "Kategori bilgileri eksik." },
                Code = "400"
            };
        }

        var category = new Category
        {
            Name = categoryCreateDto.Name,
            Description = categoryCreateDto.Description,
            ParentCategoryId = categoryCreateDto.ParentCategoryId,
            CreatedBy = createdBy,
            UpdatedBy = createdBy
        };

        try
        {
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultDto
            {
                Success = true,
                Message = "Kategori başarıyla oluşturuldu.",
                Data = category.Id,
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

    public async Task<ResultDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var categoryDto = await _context.Categories
            .AsNoTracking()
            .Where(c => c.IsActive && c.Id == id)
            .Select(c => new GetByIdCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategoryName = c.ParentCategory.Name,
                SubCategories = c.SubCategories
                    .Where(sc => sc.IsActive)
                    .Select(sc => new GetByIdCategoryDto 
                    {
                        Id = sc.Id,
                        Name = sc.Name,
                        Description = sc.Description
                    }).ToList(),
                Products = c.Products
                    .Where(p => p.IsActive)
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    }).ToList(),
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                DeletedAt = c.DeletedAt,
                IsActive = c.IsActive,
                CreatedBy = c.CreatedBy,
                UpdatedBy = c.UpdatedBy,
                DeletedBy = c.DeletedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (categoryDto == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Kategori bulunamadı.",
                ValidationErrors = new List<string> { "Kategori ID'si geçerli değil." },
                Code = "404"
            };
        }

        return new ResultDto
        {
            Success = true,
            Message = "Kategori başarıyla getirildi.",
            Data = categoryDto,
            Code = "200"
        };
    }

    public async Task<ResultDto> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        var categoryDtos = await _context.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Select(c => new 
            {
                c.Id,
                c.Name
            })
            .ToListAsync(cancellationToken);

        return new ResultDto
        {
            Success = true,
            Message = "Kategoriler başarıyla getirildi.",
            Data = categoryDtos,
            Code = "200"
        };
    }

    public async Task<ResultDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryUpdateDto, string updatedBy, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);

        if (category == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Kategori bulunamadı.",
                ValidationErrors = new List<string> { "Geçerli kategori bulunamadı." },
                Code = "404"
            };
        }

        category.Name = categoryUpdateDto.Name;
        category.Description = categoryUpdateDto.Description;
        category.ParentCategoryId = categoryUpdateDto.ParentCategoryId;
        category.UpdatedAt = DateTime.UtcNow;
        category.UpdatedBy = updatedBy;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return new ResultDto
            {
                Success = true,
                Message = "Kategori başarıyla güncellendi.",
                Data = category.Id,
                Code = "200"
            };
        }
        catch (Exception ex)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Kategori güncellenemedi.",
                ErrorDetails = ex.Message,
                Code = "500"
            };
        }
    }

    public async Task<ResultDto> DeleteCategoryAsync(Guid id, string deletedBy, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);

        if (category == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Kategori bulunamadı.",
                ValidationErrors = new List<string> { "Geçerli kategori bulunamadı." },
                Code = "404"
            };
        }

        category.IsActive = false;
        category.DeletedAt = DateTime.UtcNow;
        category.DeletedBy = deletedBy;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return new ResultDto
            {
                Success = true,
                Message = "Kategori başarıyla silindi.",
                Data = category.Id,
                Code = "200"
            };
        }
        catch (Exception ex)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Kategori silinemedi.",
                ErrorDetails = ex.Message,
                Code = "500"
            };
        }
    }
}
