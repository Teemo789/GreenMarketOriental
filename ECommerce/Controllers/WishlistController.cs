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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost]
        public async Task<ActionResult> PerformWishlistAction([FromBody] WishlistActionModel model)
        {
            var result = await _wishlistService.PerformWishlistAction(model);
            return result;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Product>>> GetWishlist(string userId)
        {
            var result = await _wishlistService.GetWishlist(userId);
            return result.Result;
        }
    }
}