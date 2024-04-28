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
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<CartResponse>> AddToCart(AddToCartModel model)
        {
            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null)
            {
                return new NotFoundResult();
            }

            var cart = await _context.Carts.Include(c => c.CartLines).FirstOrDefaultAsync(c => c.UserId == model.UserId);

            if (cart == null)
            {
                cart = new Cart { UserId = model.UserId };
                _context.Carts.Add(cart);
            }

            var cartLine = cart.CartLines?.FirstOrDefault(ci => ci.ProductId == model.ProductId);

            if (cartLine != null)
            {
                cartLine.Quantity += model.Quantity;
            }
            else
            {
                cartLine = new CartLine { ProductId = model.ProductId, Quantity = model.Quantity, Product = product, UserId = model.UserId };
                if (cart.CartLines == null)
                {
                    cart.CartLines = new List<CartLine>();
                }
                cart.CartLines.Add(cartLine);
            }

            await _context.SaveChangesAsync();

            var cartResponse = new CartResponse
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartLines = cart.CartLines.Select(cl => new CartLineResponse
                {
                    Id = cl.Id,
                    UserId = cl.UserId,
                    Quantity = cl.Quantity,
                    ProductId = cl.ProductId,
                    TotalPrice = cl.TotalPrice,
                    Product = new ProductResponse
                    {
                        Id = cl.Product.Id,
                        Name = cl.Product.Name,
                        Description = cl.Product.Description,
                        Price = cl.Product.Price,
                        ImageUrl = cl.Product.ImageUrl
                    }
                }).ToList()
            };

            return new OkObjectResult(cartResponse);
        }

        public async Task<ActionResult<CartResponse>> GetCart(string userId)
        {
            var cart = await _context.Carts.Include(c => c.CartLines)
                                            .ThenInclude(ci => ci.Product)
                                            .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new NotFoundResult();
            }

            var cartResponse = new CartResponse
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartLines = cart.CartLines.Select(cl => new CartLineResponse
                {
                    Id = cl.Id,
                    UserId = cl.UserId,
                    Quantity = cl.Quantity,
                    ProductId = cl.ProductId,
                    TotalPrice = cl.TotalPrice,
                    Product = new ProductResponse
                    {
                        Id = cl.Product.Id,
                        Name = cl.Product.Name,
                        Description = cl.Product.Description,
                        Price = cl.Product.Price,
                        ImageUrl = cl.Product.ImageUrl
                    }
                }).ToList()
            };

            return new OkObjectResult(cartResponse);
        }

        public async Task<ActionResult> RemoveFromCart(CartLine item)
        {
            var cart = await _context.Carts.Include(c => c.CartLines)
                                            .FirstOrDefaultAsync(c => c.UserId == item.UserId);

            if (cart == null)
            {
                return new NotFoundResult();
            }

            var cartLine = cart.CartLines.FirstOrDefault(ci => ci.ProductId == item.ProductId);

            if (cartLine == null)
            {
                return new NotFoundResult();
            }

            if (cartLine.Quantity > item.Quantity)
            {
                cartLine.Quantity -= item.Quantity;
            }
            else
            {
                cart.CartLines.Remove(cartLine);
            }

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult<decimal>> GetTotalValue(string userId)
        {
            var cart = await _context.Carts.Include(c => c.CartLines)
                                            .ThenInclude(ci => ci.Product)
                                            .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new NotFoundResult();
            }

            var totalValue = cart.ComputeTotalValue();

            return new OkObjectResult(totalValue);
        }
    }
}