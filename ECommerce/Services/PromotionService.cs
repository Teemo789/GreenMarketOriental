using ECommerce.Entities;
using ECommerce.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class PromotionService : IPromotionService
    {

        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public PromotionService(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public void CreatePromotion(Promotion promotion)
        {
            promotion.IdPromotion = Guid.NewGuid().ToString();
            _context.Promotions.Add(promotion);
            _context.SaveChanges();
        }

        public async Task<ActionResult<Promotion>> GetPromotionById(string promotionId)
        {
            if (!Guid.TryParse(promotionId, out Guid id))
            {
                return new NotFoundResult();
            }

            var promotion = await _context.Promotions
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.IdPromotion == promotionId);

            if (promotion == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(promotion);
        }

        public async Task<List<Promotion>> GetAllPromotions()
        {
            return await _context.Promotions.Include(p => p.Products).ToListAsync();
        }


        public async Task AddProductToPromotion(string promotionId, int productId, decimal reductionAmount)
        {
            Promotion promotion = await _context.Promotions.Include(p => p.Products).FirstOrDefaultAsync(p => p.IdPromotion == promotionId);

            if (promotion != null)
            {
                // Récupérer le produit par ID
                Product product = await _context.Products.FindAsync(productId);

                if (product != null)
                {
                    // Ajouter le produit à la promotion avec la réduction spécifiée
                    promotion.Products.Add(new ProductInPromotion
                    {
                        ProductId = productId, // Passer directement la valeur de l'ID du produit (Passet l'ID directement)
                        ReductionAmount = reductionAmount,
                        Product = product, // Associer le produit
                        Promotion = promotion // Associer la promotion
                    });

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Le produit spécifié n'a pas été trouvé.");
                }
            }
        }


    }
}