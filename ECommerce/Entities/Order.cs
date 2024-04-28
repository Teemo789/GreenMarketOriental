namespace ECommerce.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } // UserId özelliği eklendi
        public bool IsPaid { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public string ShipZip { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual Payment Payment { get; set; }
    }

}
