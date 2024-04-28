using ECommerce.Entities;
using ECommerce.IServices;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Classes
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> CreateOrder(OrderModel model)
        {
            var cart = await _context.Carts.Include(c => c.CartLines)
                                            .ThenInclude(ci => ci.Product)
                                            .FirstOrDefaultAsync(c => c.UserId == model.UserId);

            if (cart == null)
            {
                return new NotFoundResult();
            }

            var order = new Order { UserId = model.UserId, OrderDate = DateTime.UtcNow, OrderLines = new List<OrderLine>() };

            foreach (var item in cart.CartLines)
            {
                var orderLine = new OrderLine
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                order.OrderLines.Add(orderLine);
            }

            order.ShipAddress = model.ShipAddress;
            order.ShipCity = model.ShipCity;
            order.ShipCountry = model.ShipCountry;
            order.ShipZip = model.ShipZip;

            order.Total = cart.ComputeTotalValue();

            order.IsPaid = false;

            var payment = new Payment { PaymentDate = DateTime.UtcNow, Amount = order.Total };
            _context.Payments.Add(payment);
            order.Payment = payment;

            _context.Orders.Add(order);
            _context.Carts.Remove(cart);

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ActionResult<List<Order>>> GetOrders(string userId)
        {
            var orders = await _context.Orders.Include(o => o.OrderLines)
                                               .ThenInclude(oi => oi.Product)
                                               .Where(o => o.UserId == userId)
                                               .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(orders);
        }

        public async Task<ActionResult<List<Order>>> GetOrderHistory(string userId)
        {
            var orders = await _context.Orders.Include(o => o.OrderLines)
                                               .ThenInclude(oi => oi.Product)
                                               .Where(o => o.UserId == userId)
                                               .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(orders);
        }

        public async Task<ActionResult<Order>> GetOrder(string userId, int orderId)
        {
            var order = await _context.Orders.Include(o => o.OrderLines)
                                              .ThenInclude(oi => oi.Product)
                                              .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId);

            if (order == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(order);
        }
    }
}
