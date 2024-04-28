using ECommerce.Entities;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.IServices
{
    public interface IWishlistService
    {
        Task<ActionResult> PerformWishlistAction(WishlistActionModel model);
        Task<ActionResult<List<Product>>> GetWishlist(string userId);
    }
}