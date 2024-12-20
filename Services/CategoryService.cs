using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto, string createdBy)
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

        var category = new Category()
        {
            Name = categoryCreateDto.Name,
            Description = categoryCreateDto.Description,
            ParentCategoryId = categoryCreateDto.ParentCategoryId,
            CreatedBy = createdBy,
            UpdatedBy = createdBy
        };

        try
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
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

    public async Task<ResultDto> GetCategoryByIdAsync(Guid id)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .Where(c => c.IsActive == true)
            .Include(c => c.SubCategories)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return new ResultDto
            {
                Success = false,
                Message = "Kategori bulunamadı.",
                ValidationErrors = new List<string> { "Kategori ID'si geçerli değil." },
                Code = "404"
            };
        }

        var categoryDto = new GetByIdCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            ParentCategoryName = category.ParentCategory?.Name,
            SubCategories = category.SubCategories.Select(sc => new GetByIdCategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                Description = sc.Description
            }).ToList(),
            Products = category.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList(),
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt,
            DeletedAt = category.DeletedAt,
            IsActive = category.IsActive,
            CreatedBy = category.CreatedBy,
            UpdatedBy = category.UpdatedBy,
            DeletedBy = category.DeletedBy
        };

        return new ResultDto
        {
            Success = true,
            Message = "Kategori başarıyla getirildi.",
            Data = categoryDto,
            Code = "200"
        };
    }

    public async Task<ResultDto> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories
        .AsNoTracking()
        .Where(c => c.IsActive == true)
        .Select(category => new
        {
            category.Id,
            category.Name
        })
            .ToListAsync();

        var categoryDtos = categories.Select(category => new
        {
            category.Id,
            category.Name
        }).ToList();

        return new ResultDto
        {
            Success = true,
            Message = "Kategoriler başarıyla getirildi.",
            Data = categoryDtos,
            Code = "200"
        };
    }

    public async Task<ResultDto> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryUpdateDto, string updatedBy)
    {
        var category = await _context.Categories.FindAsync(id);

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
            await _context.SaveChangesAsync();
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

    public async Task<ResultDto> DeleteCategoryAsync(Guid id, string deletedBy)
    {
        var category = await _context.Categories.FindAsync(id);

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
            await _context.SaveChangesAsync();
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
