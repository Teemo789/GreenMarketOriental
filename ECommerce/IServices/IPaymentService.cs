using ECommerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.IServices
{
    public interface IPaymentService
    {
        Task<ActionResult> CreatePayment(Payment payment);
        Task<ActionResult> GetPayment(int id);
        Task<ActionResult> PayOrder(int orderId);
    }
}
