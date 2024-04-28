using ECommerce.Entities;
using ECommerce.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePayment(Payment payment)
        {
            return await _paymentService.CreatePayment(payment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPayment(int id)
        {
            return await _paymentService.GetPayment(id);
        }

        [HttpPost("pay/{orderId}")]
        public async Task<ActionResult> PayOrder(int orderId)
        {
            return await _paymentService.PayOrder(orderId);
        }
    }
}