using ECommerce.Entities;
using ECommerce.IServices;
using ECommerce.Models;
using ECommerce.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Classes
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetProductById), "Product", new { id = product.Id }, product);
        }

        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return new NotFoundResult();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.ImageUrl = product.ImageUrl;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(product);
        }

        public async Task<ActionResult> AddProductToCategory(int productId, int categoryId)
        {
            var product = await _context.Products.FindAsync(productId);
            var category = await _context.Categories.FindAsync(categoryId);

            if (product == null || category == null)
            {
                return new NotFoundResult();
            }

            product.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var products = await _context.Products
                .Where(p => p.Categories.Any(c => c.Name == category))
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(products);
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            var products = await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(products);
        }

        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string keyword)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(keyword))
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(products);
        }

        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts(ProductFilter productFilter)
        {
            var query = _context.Products.AsQueryable();

            if (productFilter.MinPrice != null)
            {
                query = query.Where(p => p.Price >= productFilter.MinPrice);
            }

            if (productFilter.MaxPrice != null)
            {
                query = query.Where(p => p.Price <= productFilter.MaxPrice);
            }

            if (productFilter.Category != null)
            {
                query = query.Where(p => p.Categories.Any(c => c.Name == productFilter.Category));
            }

            var products = await query.ToListAsync();

            if (products == null || !products.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(products);
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            if (products == null || !products.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(products);
        }
    }
}   