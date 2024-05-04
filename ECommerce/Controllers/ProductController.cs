using ECommerce.Entities;
using ECommerce.IServices;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPromotionService _promotionService;
        private readonly IReductionCalculationService _reductionCalculationService;

        public ProductController(IProductService productService, IPromotionService promotionService, IReductionCalculationService reductionCalculationService)
        {
            _productService = productService;
            _promotionService = promotionService;
            _reductionCalculationService = reductionCalculationService;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            var result = await _productService.AddProduct(product);
            return result;
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
            if (result.Value != null)
            {
                var product = result.Value;

                var promotions = await _promotionService.GetAllPromotions();

                foreach (var promotion in promotions)
                {
                    foreach (var productInPromotion in promotion.Products)
                    {
                        if (productInPromotion.ProductId == product.Id)
                        {
                            // Calcul de la réduction en pourcentage
                            decimal reductionPercentage = productInPromotion.ReductionAmount;

                            // Calcul du montant de réduction
                            decimal reductionAmount = _reductionCalculationService.CalculateReductionAmount(reductionPercentage, product.Price);

                            // Calcul du nouveau prix avec la réduction de la promotion
                            decimal newPrice = product.Price - reductionAmount;

                            // Assurer que le prix réduit n'est pas inférieur à zéro
                            product.Price = Math.Max(0, newPrice);
                        }
                    }
                }
                return product;
            }
            else
            {
                return result.Result;
            }
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
            return result;
        }

        [HttpGet("price")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            var result = await _productService.GetProductsByPrice(minPrice, maxPrice);
            return result;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string keyword)
        {
            var result = await _productService.SearchProducts(keyword);
            return result;
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts(ProductFilter productFilter)
        {
            var result = await _productService.FilterProducts(productFilter);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return result;
        }
    }
}
