using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // Kategori oluşturma
    [HttpPost]
    public async Task<ActionResult<ResultDto>> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
    {
        var createdBy = "system"; // Kullanıcı adı burada geçerli bir şekilde alınmalı
        var result = await _categoryService.CreateCategoryAsync(categoryCreateDto, createdBy, cancellationToken);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // ID'ye göre kategori getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCategoryDto>> GetCategoryById(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        if (category == null)
        {
            return NotFound(new { Message = "Kategori bulunamadı." });
        }

        return Ok(category);
    }

    // Tüm kategorileri getirme
    [HttpGet]
    public async Task<ActionResult<List<GetByIdCategoryDto>>> GetAllCategories(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllCategoriesAsync(cancellationToken);
        return Ok(categories);
    }

    // Kategori güncelleme
    [HttpPut("{id}")]
    public async Task<ActionResult<ResultDto>> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken)
    {
        var updatedBy = "system"; // Kullanıcı adı burada geçerli bir şekilde alınmalı
        var result = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto, updatedBy, cancellationToken);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // Kategori silme
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResultDto>> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var deletedBy = "system"; // Kullanıcı adı burada geçerli bir şekilde alınmalı
        var result = await _categoryService.DeleteCategoryAsync(id, deletedBy, cancellationToken);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
