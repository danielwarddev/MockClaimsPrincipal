using Microsoft.AspNetCore.Mvc;

namespace MockClaimsPrincipal.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("products")]
    public async Task<Product[]> GetProducts()
    {
        var isAdmin = HttpContext.User.IsInRole("admin");

        return isAdmin ?
            await _productService.GetAllProducts() :
            await _productService.GetPublicProducts();
    }
}
