using ECommerce.Entities;
using ECommerce.IServices;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            var result = await _productService.AddProduct(product);
            return result.Result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var result = await _productService.UpdateProduct(id, product);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var result = await _productService.GetProductById(id);
            return result.Result;
        }

        [HttpPost("{productId}/categories/{categoryId}")]
        public async Task<ActionResult> AddProductToCategory(int productId, int categoryId)
        {
            var result = await _productService.AddProductToCategory(productId, categoryId);
            return result;
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var result = await _productService.GetProductsByCategory(category);
            return result.Result;
        }

        [HttpGet("price")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            var result = await _productService.GetProductsByPrice(minPrice, maxPrice);
            return result.Result;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string keyword)
        {
            var result = await _productService.SearchProducts(keyword);
            return result.Result;
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts(ProductFilter productFilter)
        {
            var result = await _productService.FilterProducts(productFilter);
            return result.Result;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return result.Result;
        }
    }
}