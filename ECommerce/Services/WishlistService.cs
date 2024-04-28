using ECommerce.Entities;
using ECommerce.IServices;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Classes
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context;

        public WishlistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> PerformWishlistAction(WishlistActionModel model)
        {
            var product = await _context.Products.FindAsync(model.ProductId);

            if (product == null)
            {
                return new NotFoundResult();
            }

            var wishlistItem = await _context.WishlistItems
                .FirstOrDefaultAsync(wi => wi.UserId == model.UserId && wi.ProductId == model.ProductId);

            if (wishlistItem != null && !model.AddToWishlist)
            {
                _context.WishlistItems.Remove(wishlistItem);
                await _context.SaveChangesAsync();
                return new OkResult();
            }

            if (wishlistItem == null && model.AddToWishlist)
            {
                wishlistItem = new WishlistItem
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    Product = product
                };
                _context.WishlistItems.Add(wishlistItem);
                await _context.SaveChangesAsync();
                return new OkResult();
            }

            return new ConflictResult();
        }

        public async Task<ActionResult<List<Product>>> GetWishlist(string userId)
        {
            var wishlistItems = await _context.WishlistItems
                .Include(wi => wi.Product)
                .Where(wi => wi.UserId == userId)
                .ToListAsync();

            if (wishlistItems == null || wishlistItems.Count == 0)
            {
                return new NotFoundResult();
            }

            var products = wishlistItems.Select(wi => wi.Product).ToList();

            return new OkObjectResult(products);
        }
    }
}