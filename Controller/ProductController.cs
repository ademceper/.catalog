using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // Ürün oluşturma
    [HttpPost]
    public async Task<ActionResult<ResultDto>> CreateProduct([FromBody] ProductCreateDto productCreateDto)
    {
        var createdBy = "system"; // Örnek: Oturum açan kullanıcıyı alabilirsiniz
        var result = await _productService.CreateProductAsync(productCreateDto, createdBy);
        if (result.Success)
        {
            return StatusCode(201, result); // Created status code
        }

        return BadRequest(result);
    }

    // Ürün güncelleme
    [HttpPut("{id}")]
    public async Task<ActionResult<ResultDto>> UpdateProduct(Guid id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        var updatedBy = "system"; // Örnek: Oturum açan kullanıcıyı alabilirsiniz
        var result = await _productService.UpdateProductAsync(id, productUpdateDto, updatedBy);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // Ürün ID'ye göre getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<ResultDto>> GetProductById(Guid id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        if (result.Success)
        {
            return Ok(result);
        }

        return NotFound(result);
    }

    // Tüm ürünleri getirme
    [HttpGet]
    public async Task<ActionResult<ResultDto>> GetAllProducts()
    {
        var result = await _productService.GetAllProductsAsync();
        return Ok(result);
    }

    // Ürün silme
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResultDto>> DeleteProduct(Guid id)
    {
        var deletedBy = "system"; // Örnek: Oturum açan kullanıcıyı alabilirsiniz
        var result = await _productService.DeleteProductAsync(id, deletedBy);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
