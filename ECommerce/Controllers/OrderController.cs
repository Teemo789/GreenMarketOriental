using ECommerce.Entities;
using ECommerce.IServices;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderModel model)
        {
            return await _orderService.CreateOrder(model);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Order>>> GetOrders(string userId)
        {
            return await _orderService.GetOrders(userId);
        }

        [HttpGet("history/{userId}")]
        public async Task<ActionResult<List<Order>>> GetOrderHistory(string userId)
        {
            return await _orderService.GetOrderHistory(userId);
        }

        [HttpGet("{userId}/{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(string userId, int orderId)
        {
            return await _orderService.GetOrder(userId, orderId);
        }
    }
}