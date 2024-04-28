using ECommerce.Entities;
using ECommerce.Models;
using ECommerce.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.IServices
{
    public interface ICartService
    {
        Task<ActionResult<CartResponse>> AddToCart(AddToCartModel model);
        Task<ActionResult<CartResponse>> GetCart(string userId);
        Task<ActionResult> RemoveFromCart(CartLine item);
        Task<ActionResult<decimal>> GetTotalValue(string userId);
    }
}
