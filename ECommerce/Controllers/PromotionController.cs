using ECommerce.Entities;
using ECommerce.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        private readonly ApplicationDbContext _context;
        private readonly IReductionCalculationService _reductionCalculationService;

        public PromotionController(IPromotionService promotionService, ApplicationDbContext context, IReductionCalculationService reductionCalculationService)
        {
            _promotionService = promotionService;
            _context = context;
            _reductionCalculationService = reductionCalculationService;
        }

        [HttpPost("{promotionId}/products/{productId}")]
        public async Task<IActionResult> AddProductToPromotion(string promotionId, int productId, decimal reductionPercentage)
        {
            Promotion promotion = await _context.Promotions
                                        .Include(p => p.Products)
                                        .FirstOrDefaultAsync(p => p.IdPromotion == promotionId);

            if (promotion == null)
            {
                throw new ArgumentException("La promotion spécifiée n'a pas été trouvée.");
            }

            // Récupération du produit par ID
            Product product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ArgumentException("Le produit spécifié n'a pas été trouvé.");
            }

            // Calcul du montant de réduction
            decimal reductionAmount = _reductionCalculationService.CalculateReductionAmount(reductionPercentage, product.Price);

            // Calcul du nouveau prix avec réduction
            decimal newPrice = product.Price - reductionAmount;

            // Assurer que le prix réduit n'est pas inférieur à zéro
            newPrice = Math.Max(0, newPrice);

            // Mise à jour du prix du produit dans la base de données
            product.Price = newPrice;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Ajout du produit à la promotion avec la réduction spécifiée
            promotion.Products.Add(new ProductInPromotion
            {
                ProductId = productId, // Passer directement la valeur de l'ID du produit
                ReductionAmount = reductionAmount
            });

            await _context.SaveChangesAsync();

            return Ok("Le produit a été ajouté à la promotion avec succès.");
        }

    }
}
