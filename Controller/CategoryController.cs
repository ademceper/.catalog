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
    public async Task<ActionResult<ResultDto>> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
    {
        var createdBy = "system"; // Kullanıcı adı burada geçerli bir şekilde alınmalı
        var result = await _categoryService.CreateCategoryAsync(categoryCreateDto, createdBy);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // ID'ye göre kategori getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdCategoryDto>> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound(new { Message = "Kategori bulunamadı." });
        }

        return Ok(category);
    }

    // Tüm kategorileri getirme
    [HttpGet]
    public async Task<ActionResult<List<GetByIdCategoryDto>>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    // Kategori güncelleme
    [HttpPut("{id}")]
    public async Task<ActionResult<ResultDto>> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdateDto)
    {
        var updatedBy = "system"; // Kullanıcı adı burada geçerli bir şekilde alınmalı
        var result = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto, updatedBy);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // Kategori silme
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResultDto>> DeleteCategory(Guid id)
    {
        var deletedBy = "system"; // Kullanıcı adı burada geçerli bir şekilde alınmalı
        var result = await _categoryService.DeleteCategoryAsync(id, deletedBy);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
