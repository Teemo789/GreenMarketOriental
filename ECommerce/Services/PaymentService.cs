using ECommerce.Entities;
using ECommerce.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ECommerce.Classes
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetPayment), "Payment", new { id = payment.Id }, payment);
        }

        public async Task<ActionResult> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(payment);
        }

        public async Task<ActionResult> PayOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return new NotFoundResult();
            }

            order.IsPaid = true;
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
