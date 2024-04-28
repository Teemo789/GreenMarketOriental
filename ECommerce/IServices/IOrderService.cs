using ECommerce.Entities;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.IServices
{
    public interface IOrderService
    {
        Task<ActionResult> CreateOrder(OrderModel model);
        Task<ActionResult<List<Order>>> GetOrders(string userId);
        Task<ActionResult<List<Order>>> GetOrderHistory(string userId);
        Task<ActionResult<Order>> GetOrder(string userId, int orderId);
    }
}
