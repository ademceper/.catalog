using Microsoft.AspNetCore.Mvc;
using System.Threading;

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
    public async Task<ActionResult<ResultDto>> CreateProduct([FromBody] ProductCreateDto productCreateDto, CancellationToken cancellationToken)
    {
        var createdBy = "system"; // Örnek: Oturum açan kullanıcıyı alabilirsiniz
        var result = await _productService.CreateProductAsync(productCreateDto, createdBy, cancellationToken);
        
        if (result.Success)
        {
            // Oluşturulan ürünün detayını çekebilmek için CreatedAtAction kullanabilirsiniz.
            // result.Data varsayılan olarak oluşturulan ürünün ID'sini içeriyor.
            return CreatedAtAction(nameof(GetProductById), new { id = result.Data }, result);
        }

        // Hata durumuna göre uygun status code
        return StatusCodeFromResult(result);
    }

    // Ürün güncelleme
    [HttpPut("{id}")]
    public async Task<ActionResult<ResultDto>> UpdateProduct(Guid id, [FromBody] ProductUpdateDto productUpdateDto, CancellationToken cancellationToken)
    {
        var updatedBy = "system";
        var result = await _productService.UpdateProductAsync(id, productUpdateDto, updatedBy, cancellationToken);

        if (result.Success)
        {
            return Ok(result);
        }

        return StatusCodeFromResult(result);
    }

    // Ürün ID'ye göre getirme
    [HttpGet("{id}")]
    public async Task<ActionResult<ResultDto>> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.GetProductByIdAsync(id, cancellationToken);
        if (result.Success)
        {
            return Ok(result);
        }

        return StatusCodeFromResult(result);
    }

    // Tüm ürünleri getirme
    [HttpGet]
    public async Task<ActionResult<ResultDto>> GetAllProducts(CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllProductsAsync(cancellationToken);
        // Bu istek genellikle her zaman başarıyla sonuçlanırsa direkt Ok döndürülebilir.
        // Ancak yine de hata kontrolü yapmak isterseniz aşağıdaki gibi yapılabilir:
        if (result.Success)
        {
            return Ok(result);
        }

        return StatusCodeFromResult(result);
    }

    // Ürün silme
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResultDto>> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var deletedBy = "system";
        var result = await _productService.DeleteProductAsync(id, deletedBy, cancellationToken);

        if (result.Success)
        {
            return Ok(result);
        }

        return StatusCodeFromResult(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<ResultDto>> SearchProducts([FromQuery] string query, CancellationToken cancellationToken)
    {
        var result = await _productService.SearchProductsAsync(query, cancellationToken);
        
        if (result.Success)
        {
            return Ok(result);
        }

        return StatusCodeFromResult(result);
    }

    /// <summary>
    /// ResultDto'dan gelen Code değerini inceleyerek uygun Http durum kodunu döndürür.
    /// </summary>
    private ActionResult<ResultDto> StatusCodeFromResult(ResultDto result)
    {
        // Burada result.Code'a göre daha ince ayrım yapabilirsiniz.
        // Örneğin Code "404" ise NotFound, "400" ise BadRequest, "500" ise InternalServerError.
        // result.Success zaten false olduğu için hata durumlarını değerlendiriyoruz.

        return result.Code switch
        {
            "404" => NotFound(result),
            "400" => BadRequest(result),
            "500" => StatusCode(500, result),
            _ => BadRequest(result) // Bilinmeyen durumlarda varsayılan olarak BadRequest dönebilir.
        };
    }
}