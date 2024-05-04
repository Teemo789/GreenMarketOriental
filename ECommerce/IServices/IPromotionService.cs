using ECommerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.IServices
{
    public interface IPromotionService
    {
        void CreatePromotion(Promotion promotion);
        Task<ActionResult<Promotion>> GetPromotionById(string promotionId);
        Task<List<Promotion>> GetAllPromotions(); // Modification ici
        Task AddProductToPromotion(string promotionId, int productId, decimal reductionAmount);
    }
}
