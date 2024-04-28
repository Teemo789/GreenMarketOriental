using ECommerce.Entities;
using ECommerce.IServices;
using ECommerce.Models;
using ECommerce.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CartResponse>> AddToCart(AddToCartModel model)
        {
            return await _cartService.AddToCart(model);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<CartResponse>> GetCart(string userId)
        {
            return await _cartService.GetCart(userId);
        }

        [HttpPut("remove")]
        public async Task<ActionResult> RemoveFromCart(CartLine item)
        {
            return await _cartService.RemoveFromCart(item);
        }

        [HttpGet("total/{userId}")]
        public async Task<ActionResult<decimal>> GetTotalValue(string userId)
        {
            return await _cartService.GetTotalValue(userId);
        }
    }
}