using System;

namespace ECommerce.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; } // Change the type to DateTime
        public decimal Amount { get; set; }
    }
}
