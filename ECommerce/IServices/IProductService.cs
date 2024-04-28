using ECommerce.Entities;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.IServices
{
    public interface IProductService
    {
        Task<ActionResult<Product>> AddProduct(Product product);
        Task<IActionResult> UpdateProduct(int id, Product product);
        Task<ActionResult<Product>> GetProductById(int id);
        Task<ActionResult> AddProductToCategory(int productId, int categoryId);
        Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category);
        Task<ActionResult<IEnumerable<Product>>> GetProductsByPrice(decimal minPrice, decimal maxPrice);
        Task<ActionResult<IEnumerable<Product>>> SearchProducts(string keyword);
        Task<ActionResult<IEnumerable<Product>>> FilterProducts(ProductFilter productFilter);
        Task<ActionResult<IEnumerable<Product>>> GetAllProducts();
    }
}